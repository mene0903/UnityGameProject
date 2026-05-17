using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 4f;

    [Header("Hop Motion")]
    public float hopHeight = 0.15f;
    public float hopSpeed = 12f;

    [Header("Animators")]
    public RuntimeAnimatorController leftAnimator;
    public RuntimeAnimatorController rightAnimator;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 moveInput;
    private Vector2 basePosition;

    private float hopTimer;
    private bool isMoving;
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        basePosition = rb.position;

        if (rightAnimator != null)
            animator.runtimeAnimatorController = rightAnimator;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(x, y).normalized;
        isMoving = moveInput != Vector2.zero;

        if (x > 0)
        {
            facingRight = true;
            if (rightAnimator != null)
                animator.runtimeAnimatorController = rightAnimator;
        }
        else if (x < 0)
        {
            facingRight = false;
            if (leftAnimator != null)
                animator.runtimeAnimatorController = leftAnimator;
        }

        animator.SetBool("isMoving", isMoving);
    }

    void FixedUpdate()
    {
        Vector2 move = moveInput * moveSpeed * Time.fixedDeltaTime;
        basePosition += move;

        Vector2 finalPosition = basePosition;

        if (isMoving)
        {
            hopTimer += Time.fixedDeltaTime * hopSpeed;

            float hopOffset = Mathf.Abs(Mathf.Sin(hopTimer)) * hopHeight;
            finalPosition.y += hopOffset;
        }
        else
        {
            hopTimer = 0f;
        }

        rb.MovePosition(finalPosition);
    }
}