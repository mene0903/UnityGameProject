using UnityEngine;

public class LeverSpeedSwitch : MonoBehaviour
{
    [Header("레버 이미지")]
    public Sprite leverOffSprite;
    public Sprite leverOnSprite;

    [Header("컨베이어")]
    public ConveyorSpawner conveyorSpawner;

    [Header("변경할 속도")]
    public float changedMoveSpeed = 10f;

    private SpriteRenderer spriteRenderer;
    private bool playerNear = false;
    private bool used = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (leverOffSprite != null)
        {
            spriteRenderer.sprite = leverOffSprite;
        }
    }

    private void Update()
    {
        if (used) return;

        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            used = true;

            if (leverOnSprite != null)
            {
                spriteRenderer.sprite = leverOnSprite;
            }

            if (conveyorSpawner != null)
            {
                conveyorSpawner.moveSpeed = changedMoveSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}