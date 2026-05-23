using UnityEngine;

public class LeverSwitch : MonoBehaviour
{
    [Header("레버 이미지")]
    public Sprite onSprite;
    public Sprite offSprite;

    [Header("없앨 레이저")]
    public GameObject laserObject;

    [Header("상호작용 키")]
    public KeyCode interactKey = KeyCode.E;

    private SpriteRenderer spriteRenderer;
    private bool playerInRange = false;
    private bool isOn = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 처음 상태는 ON
        isOn = true;
        spriteRenderer.sprite = onSprite;

        if (laserObject != null)
            laserObject.SetActive(true);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            TurnOffLever();
        }
    }

    void TurnOffLever()
    {
        if (!isOn) return;

        isOn = false;

        // 레버 이미지 OFF로 변경
        spriteRenderer.sprite = offSprite;

        // 레이저 제거
        if (laserObject != null)
            laserObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}