using UnityEngine;

public class RobotChaseNoArmLegScene : MonoBehaviour
{
    [Header("플레이어")]
    public Transform player;

    [Header("로봇 몸체")]
    public Transform robotBody;

    [Header("시야 이미지")]
    public Transform viewConeVisual;
    public Vector3 viewConeOffset = Vector3.zero;

    [Header("느낌표")]
    public GameObject alertIcon;
    public Vector3 alertOffset = new Vector3(0, 0.8f, 0);

    [Header("시야 설정")]
    public float viewDistance = 5f;
    public float viewAngle = 60f;

    [Header("벽 감지 설정")]
    public LayerMask obstacleLayer;

    [Header("두리번 설정")]
    public float lookAngle = 45f;
    public float lookSpeed = 2f;

    [Header("360도 회전 설정")]
    public float directionChangeTime = 3f;
    public float baseTurnSpeed = 120f;

    [Header("로봇 회전 보정")]
    public float robotRotationOffset = 0f;

    [Header("총알")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 1f;

    [Header("감지 상태")]
    public bool playerDetected = false;

    private Vector2 currentLookDirection;

    private float baseAngle = 0f;
    private float targetBaseAngle = 0f;
    private float timer = 0f;
    private float shootTimer = 0f;

    void Start()
    {
        if (player == null)
        {
            GameObject target =
                GameObject.FindGameObjectWithTag("Player");

            if (target != null)
                player = target.transform;
        }

        currentLookDirection = Vector2.right;

        if (alertIcon != null)
            alertIcon.SetActive(false);
    }

    void Update()
    {
        UpdateTargetDirection();
        SmoothRotateBaseDirection();
        RotateViewDirection();

        UpdateViewConeVisual();
        UpdateRobotBodyRotation();

        shootTimer -= Time.deltaTime;

        if (player == null) return;

        playerDetected = IsPlayerInView();

        UpdateAlertIcon();

        if (playerDetected)
        {
            ShootAtPlayer();
        }
    }

    void UpdateTargetDirection()
    {
        timer += Time.deltaTime;

        if (timer >= directionChangeTime)
        {
            timer = 0f;

            targetBaseAngle += 90f;

            if (targetBaseAngle >= 360f)
                targetBaseAngle = 0f;
        }
    }

    void SmoothRotateBaseDirection()
    {
        baseAngle = Mathf.MoveTowardsAngle(
            baseAngle,
            targetBaseAngle,
            baseTurnSpeed * Time.deltaTime
        );
    }

    void RotateViewDirection()
    {
        float scanAngle =
            Mathf.Sin(Time.time * lookSpeed) * lookAngle;

        float finalAngle = baseAngle + scanAngle;

        currentLookDirection = AngleToDirection(finalAngle);
    }

    Vector2 AngleToDirection(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;

        return new Vector2(
            Mathf.Cos(rad),
            Mathf.Sin(rad)
        ).normalized;
    }

    bool IsPlayerInView()
    {
        Vector2 viewOrigin =
            (Vector2)transform.position + (Vector2)viewConeOffset;

        Vector2 toPlayer =
            (Vector2)player.position - viewOrigin;

        float distanceToPlayer = toPlayer.magnitude;

        // 거리 밖이면 감지 X
        if (distanceToPlayer > viewDistance)
            return false;

        // 시야 각도 밖이면 감지 X
        float angle =
            Vector2.Angle(currentLookDirection, toPlayer);

        if (angle > viewAngle / 2f)
            return false;

        // 벽/박스콜라이더에 가려졌는지 검사
        RaycastHit2D hit = Physics2D.Raycast(
            viewOrigin,
            toPlayer.normalized,
            distanceToPlayer,
            obstacleLayer
        );

        // 중간에 벽이 있으면 감지 X
        if (hit.collider != null)
            return false;

        return true;
    }

    void ShootAtPlayer()
    {
        if (shootTimer > 0f) return;
        if (bulletPrefab == null) return;
        if (firePoint == null) return;

        Vector2 direction =
            ((Vector2)player.position - (Vector2)firePoint.position).normalized;

        GameObject bullet =
            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity
            );

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.Shoot(direction);
        }

        shootTimer = shootInterval;
    }

    void UpdateViewConeVisual()
    {
        if (viewConeVisual == null) return;

        Vector2 dir = currentLookDirection.normalized;

        viewConeVisual.position =
            transform.position + viewConeOffset;

        float angle =
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        viewConeVisual.rotation =
            Quaternion.Euler(0, 0, angle);

        float scale = viewDistance / 5f;

        viewConeVisual.localScale =
            new Vector3(scale, scale, 1f);
    }

    void UpdateRobotBodyRotation()
    {
        if (robotBody == null) return;

        Vector2 dir = currentLookDirection.normalized;

        float angle =
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        robotBody.rotation =
            Quaternion.Euler(0, 0, angle + robotRotationOffset);
    }

    void UpdateAlertIcon()
    {
        if (alertIcon == null) return;

        alertIcon.SetActive(playerDetected);

        if (playerDetected)
        {
            alertIcon.transform.position =
                transform.position + alertOffset;

            alertIcon.transform.rotation =
                Quaternion.identity;
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector2 dir = Application.isPlaying
            ? currentLookDirection.normalized
            : Vector2.right;

        Vector3 origin =
            transform.position + viewConeOffset;

        float halfAngle = viewAngle / 2f;

        Vector2 leftDir = RotateVector(dir, halfAngle);
        Vector2 rightDir = RotateVector(dir, -halfAngle);

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(
            origin,
            origin + (Vector3)(leftDir * viewDistance)
        );

        Gizmos.DrawLine(
            origin,
            origin + (Vector3)(rightDir * viewDistance)
        );

        int segments = 30;

        Vector3 prevPoint =
            origin + (Vector3)(rightDir * viewDistance);

        for (int i = 1; i <= segments; i++)
        {
            float angle =
                -halfAngle + (viewAngle / segments) * i;

            Vector2 nextDir =
                RotateVector(dir, angle);

            Vector3 nextPoint =
                origin + (Vector3)(nextDir * viewDistance);

            Gizmos.DrawLine(prevPoint, nextPoint);

            prevPoint = nextPoint;
        }

        Gizmos.color = Color.red;

        Gizmos.DrawLine(
            origin,
            origin + (Vector3)(dir * viewDistance)
        );
    }

    Vector2 RotateVector(Vector2 v, float degree)
    {
        float rad = degree * Mathf.Deg2Rad;

        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
}