using UnityEngine;

public class LeverController : MonoBehaviour
{
    [Header("레버 이미지")]
    public Sprite leverOnSprite;
    public Sprite leverOffSprite;

    [Header("사라질 빨간 선")]
    public GameObject redLine;

    private SpriteRenderer spriteRenderer;

    private bool isPlayerNear = false;
    private bool isOn = true;

    // OFF 기준 원래 크기
    private Vector3 offScale;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 현재 크기를 OFF 기준 크기로 저장
        offScale = transform.localScale;

        // 시작은 ON 상태
        spriteRenderer.sprite = leverOnSprite;

        // ON 이미지는 크니까 자동 축소
        transform.localScale = new Vector3(
            offScale.x * 0.341f,
            offScale.y * 0.465f,
            offScale.z
        );

        if (redLine != null)
            redLine.SetActive(true);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (isOn)
            {
                TurnOffLever();
            }
        }
    }

    void TurnOffLever()
    {
        isOn = false;

        // OFF 이미지로 변경
        spriteRenderer.sprite = leverOffSprite;

        // OFF 원래 크기로 복귀
        transform.localScale = offScale;

        // 빨간 선 제거
        if (redLine != null)
            redLine.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}