using UnityEngine;

public class MovingGuardRobot : MonoBehaviour
{
    [Header("자식 오브젝트 연결")]
    public GameObject viewCone;
    public GameObject alertIcon;
    public Transform robotBody;
    public Transform firePoint;

    [Header("플레이어")]
    public Transform player;

    [Header("순찰 지점")]
    public Transform pointA;
    public Transform pointB;

    [Header("이동 설정")]
    public float moveSpeed = 2f;

    [Header("감지 거리")]
    public float detectDistance = 5f;

    private Transform targetPoint;
    private bool playerDetected = false;

    void Start()
    {
        // 시작 위치를 A로 맞춤
        if (pointA != null)
        {
            transform.position = new Vector3(
                pointA.position.x,
                pointA.position.y,
                transform.position.z
            );
        }

        targetPoint = pointB;

        if (alertIcon != null)
            alertIcon.SetActive(false);
    }

    void Update()
    {
        Patrol();
        DetectPlayer();
    }

    void Patrol()
    {
        if (pointA == null || pointB == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;

            FlipRobot();
        }
    }

    void FlipRobot()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void DetectPlayer()
    {
        if (player == null) return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance <= detectDistance)
        {
            if (!playerDetected)
            {
                playerDetected = true;

                if (alertIcon != null)
                    alertIcon.SetActive(true);

                Debug.Log("플레이어 발견");
            }
        }
        else
        {
            if (playerDetected)
            {
                playerDetected = false;

                if (alertIcon != null)
                    alertIcon.SetActive(false);

                Debug.Log("플레이어 놓침");
            }
        }
    }
}