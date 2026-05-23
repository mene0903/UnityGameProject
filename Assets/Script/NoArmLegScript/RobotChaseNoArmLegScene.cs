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

    [Header("감시 움직임 설정")]
    public float scanAngle = 12f;
    public float scanSpeed = 0.8f;
    public float watchTime = 3f;
    public float pauseTime = 0.8f;
    public float turnSpeed = 25f;
    public float turnAmount = 90f;

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
    private float targetAngle = 0f;
    private float stateTimer = 0f;
    private float shootTimer = 0f;

    private float currentScanOffset = 0f;

    private enum RobotState
    {
        Watch,
        PauseBeforeTurn,
        Turn,
        PauseAfterTurn
    }

    private RobotState currentState = RobotState.Watch;

    private void Start()
    {
        if (player == null)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");

            if (target != null)
                player = target.transform;
        }

        currentLookDirection = AngleToDirection(baseAngle);

        if (alertIcon != null)
            alertIcon.SetActive(false);
    }

    private void Update()
    {
        UpdateState();
        UpdateLookDirection();
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

    private void UpdateState()
    {
        stateTimer += Time.deltaTime;

        switch (currentState)
        {
            case RobotState.Watch:
                if (stateTimer >= watchTime)
                {
                    ChangeState(RobotState.PauseBeforeTurn);
                }
                break;

            case RobotState.PauseBeforeTurn:
                if (stateTimer >= pauseTime)
                {
                    targetAngle = baseAngle + turnAmount;

                    if (targetAngle >= 360f)
                        targetAngle -= 360f;

                    ChangeState(RobotState.Turn);
                }
                break;

            case RobotState.Turn:
                baseAngle = Mathf.MoveTowardsAngle(
                    baseAngle,
                    targetAngle,
                    turnSpeed * Time.deltaTime
                );

                if (Mathf.Abs(Mathf.DeltaAngle(baseAngle, targetAngle)) < 0.5f)
                {
                    baseAngle = targetAngle;
                    ChangeState(RobotState.PauseAfterTurn);
                }
                break;

            case RobotState.PauseAfterTurn:
                if (stateTimer >= pauseTime)
                {
                    ChangeState(RobotState.Watch);
                }
                break;
        }
    }

    private void ChangeState(RobotState newState)
    {
        currentState = newState;
        stateTimer = 0f;
    }

    private void UpdateLookDirection()
    {
        float targetScanOffset = 0f;

        if (currentState == RobotState.Watch)
        {
            targetScanOffset =
                Mathf.Sin(Time.time * scanSpeed) * scanAngle;
        }

        // 스캔하다가 멈출 때 갑자기 정면 보는 문제 방지
        currentScanOffset = Mathf.Lerp(
            currentScanOffset,
            targetScanOffset,
            Time.deltaTime * 4f
        );

        float finalAngle = baseAngle + currentScanOffset;

        currentLookDirection = AngleToDirection(finalAngle);
    }
    private Vector2 AngleToDirection(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;

        return new Vector2(
            Mathf.Cos(rad),
            Mathf.Sin(rad)
        ).normalized;
    }

    private bool IsPlayerInView()
    {
        Vector2 viewOrigin =
            (Vector2)transform.position + (Vector2)viewConeOffset;

        Vector2 toPlayer =
            (Vector2)player.position - viewOrigin;

        float distanceToPlayer = toPlayer.magnitude;

        if (distanceToPlayer > viewDistance)
            return false;

        float angle =
            Vector2.Angle(currentLookDirection, toPlayer);

        if (angle > viewAngle / 2f)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(
            viewOrigin,
            toPlayer.normalized,
            distanceToPlayer,
            obstacleLayer
        );

        if (hit.collider != null)
            return false;

        return true;
    }

    private void ShootAtPlayer()
    {
        if (shootTimer > 0f) return;
        if (bulletPrefab == null) return;
        if (firePoint == null) return;

        Vector2 direction =
            ((Vector2)player.position - (Vector2)firePoint.position).normalized;

        GameObject bullet = Instantiate(
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

    private void UpdateViewConeVisual()
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

    private void UpdateRobotBodyRotation()
    {
        if (robotBody == null) return;

        robotBody.rotation =
            Quaternion.Euler(0, 0, baseAngle + robotRotationOffset);
    }

    private void UpdateAlertIcon()
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
}