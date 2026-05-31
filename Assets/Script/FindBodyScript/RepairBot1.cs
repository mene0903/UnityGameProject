using UnityEngine;

public class RepairBot1 : MonoBehaviour
{
    [Header("Detection")]
    public float detectRange = 3f;
    public float giveUpRange = 7f;

    [Header("Movement")]
    public float chaseSpeed = 2f;
    public float returnSpeed = 1.5f;
    public float patrolRange = 2f;
    public float patrolSpeed = 1f;

    [Header("Attack")]
    public bool stopOnBodyFound = true;
    public GameObject wrenchPrefab;
    public float throwCooldown = 2f;
    public float arcHeight = 2f;

    private Transform player;
    private Vector3 startPos;
    private bool isChasing = false;
    private float patrolDir = 1f;
    private float lastThrowTime = 0f;
    private Animator animator;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;

        startPos = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");

            if (playerObj != null)
                player = playerObj.transform;
        }

        if (player == null)
            return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (!isChasing && dist <= detectRange)
            isChasing = true;

        if (isChasing)
        {
            if (dist >= giveUpRange)
            {
                isChasing = false;
            }
            else
            {
                ChasePlayer();
                TryThrowWrench();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        float targetX = startPos.x + patrolRange * patrolDir;

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(targetX, transform.position.y, transform.position.z),
            patrolSpeed * Time.deltaTime);

        if (patrolDir > 0)
        {
            animator.SetInteger("Direction", 2);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            animator.SetInteger("Direction", 2);
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
            patrolDir *= -1f;
    }

    void ChasePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        transform.Translate(dir * chaseSpeed * Time.deltaTime);

        if (dir.y < -0.5f)
        {
            animator.SetInteger("Direction", 0);
        }
        else if (dir.y > 0.5f)
        {
            animator.SetInteger("Direction", 1);
        }
        else if (dir.x > 0)
        {
            animator.SetInteger("Direction", 2);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            animator.SetInteger("Direction", 2);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void TryThrowWrench()
    {
        if (Time.time - lastThrowTime >= throwCooldown)
        {
            ThrowWrench(player.position);
            lastThrowTime = Time.time;
        }
    }

    void ThrowWrench(Vector3 targetPos)
    {
        if (wrenchPrefab == null)
            return;

        GameObject wrench = Instantiate(
            wrenchPrefab,
            transform.position,
            Quaternion.identity);

        Wrench1 wrenchScript = wrench.GetComponent<Wrench1>();

        if (wrenchScript != null)
        {
            wrenchScript.Launch(
                transform.position,
                targetPos);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, giveUpRange);
    }
}