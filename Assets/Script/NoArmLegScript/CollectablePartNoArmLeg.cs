using UnityEngine;

public class CollectablePart : MonoBehaviour
{
    [Header("획득 시 가운데 팝업 이미지")]
    public Sprite itemSprite;

    [Header("획득 후 적용할 Animator Controller")]
    public RuntimeAnimatorController newAnimatorController;

    [Header("상호작용 키")]
    public KeyCode interactKey = KeyCode.E;

    private bool playerInRange = false;

    private ItemPopupUI popupUI;

    private Animator playerAnimator;

    void Start()
    {
        popupUI = FindObjectOfType<ItemPopupUI>();

        if (itemSprite == null)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            if (sr != null)
            {
                itemSprite = sr.sprite;
            }
        }
    }

    void Update()
    {
        if (playerInRange &&
            Input.GetKeyDown(interactKey))
        {
            Collect();
        }
    }

    void Collect()
    {
        if (popupUI != null)
        {
            popupUI.ShowItem(itemSprite);
        }

        if (playerAnimator != null &&
            newAnimatorController != null)
        {
            playerAnimator.runtimeAnimatorController =
                newAnimatorController;

            Debug.Log("Animator Controller 변경 완료");

            if (GameProgressManager.Instance != null)
            {
                GameProgressManager.Instance.SetHasLegs();
            }
        }
        else
        {
            Debug.LogWarning("Animator 또는 Controller가 비어있음");
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            playerAnimator =
                other.GetComponent<Animator>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            playerAnimator = null;
        }
    }
}