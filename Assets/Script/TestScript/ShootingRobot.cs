using UnityEngine;

public class ShootingRobot : MonoBehaviour
{
    [Header("플레이어")]
    public Transform player;

    [Header("감시 로봇")]
    public RobotChase watchRobot;

    [Header("총알")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 1f;

    [Header("회전 설정")]
    public bool rotateToPlayer = true;

    private float shootTimer = 0f;

    void Start()
    {
        if (player == null)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");

            if (target != null)
            {
                player = target.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;
        if (watchRobot == null) return;

        shootTimer -= Time.deltaTime;

        // 감시 로봇이 플레이어를 발견했을 때만 작동
        if (watchRobot.playerDetected)
        {
            LookAtPlayer();

            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootInterval;
            }
        }
    }

    void LookAtPlayer()
    {
        if (!rotateToPlayer) return;

        Vector2 direction = player.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;
        if (firePoint == null) return;

        Vector2 direction =
            ((Vector2)player.position - (Vector2)firePoint.position).normalized;

        GameObject bullet =
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.Shoot(direction);
        }
    }
}   