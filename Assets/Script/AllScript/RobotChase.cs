using UnityEngine;

public class RobotChase : MonoBehaviour
{
    [Header("플레이어")]
    public Transform player;

    [Header("시야 이미지")]
    public Transform viewConeVisual;

    [Header("시야 설정")]
    public float viewDistance = 5f;
    public float viewAngle = 60f;

    [Header("고개 회전")]
    public float lookAngle = 45f;
    public float lookSpeed = 2f;

    [Header("이동")]
    public float moveSpeed = 2f;

    [Header("기본 보는 방향")]
    public Vector2 baseLookDirection = Vector2.right;

    [Header("감지 상태")]
    public bool playerDetected = false;

    Rigidbody2D rb;
    Vector2 currentLookDirection;
    bool isChasing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");

            if (target != null)
                player = target.transform;
        }

        currentLookDirection = baseLookDirection.normalized;
    }

    void Update()
    {
        RotateViewDirection();
        UpdateViewConeVisual();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        if (IsPlayerInView())
        {
            playerDetected = true;
            isChasing = true;
        }
        else
        {
            playerDetected = false;
        }

        if (isChasing)
        {
            Vector2 direction = ((Vector2)player.position - rb.position).normalized;

            rb.MovePosition(
                rb.position + direction * moveSpeed * Time.fixedDeltaTime
            );
        }
    }

    void RotateViewDirection()
    {
        float angle = Mathf.Sin(Time.time * lookSpeed) * lookAngle;
        currentLookDirection = RotateVector(baseLookDirection.normalized, angle);
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

    bool IsPlayerInView()
    {
        Vector2 toPlayer = player.position - transform.position;

        if (toPlayer.magnitude > viewDistance)
            return false;

        float angle = Vector2.Angle(currentLookDirection, toPlayer);

        return angle <= viewAngle / 2f;
    }

    void UpdateViewConeVisual()
    {
        if (viewConeVisual == null) return;

        Vector2 dir = currentLookDirection.normalized;

        viewConeVisual.position = transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        viewConeVisual.rotation = Quaternion.Euler(0, 0, angle);

        float scale = viewDistance / 5f;

        viewConeVisual.localScale = new Vector3(scale, scale, 1f);
    }

    void OnDrawGizmosSelected()
    {
        Vector2 dir;

        if (Application.isPlaying)
            dir = currentLookDirection.normalized;
        else
            dir = baseLookDirection.normalized;

        Vector3 origin = transform.position;

        float halfAngle = viewAngle / 2f;

        Vector2 leftDir = RotateVector(dir, halfAngle);
        Vector2 rightDir = RotateVector(dir, -halfAngle);

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(origin, origin + (Vector3)(leftDir * viewDistance));
        Gizmos.DrawLine(origin, origin + (Vector3)(rightDir * viewDistance));

        int segments = 30;
        Vector3 prevPoint = origin + (Vector3)(rightDir * viewDistance);

        for (int i = 1; i <= segments; i++)
        {
            float angle = -halfAngle + (viewAngle / segments) * i;
            Vector2 nextDir = RotateVector(dir, angle);

            Vector3 nextPoint = origin + (Vector3)(nextDir * viewDistance);

            Gizmos.DrawLine(prevPoint, nextPoint);

            prevPoint = nextPoint;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + (Vector3)(dir * viewDistance));
    }
}