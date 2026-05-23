using UnityEngine;

public class CollectablePart1 : MonoBehaviour
{
    [Header("È¹µæ ½Ã °¡¿îµ¥ ÆË¾÷ ÀÌ¹ÌÁö")]
    public Sprite itemSprite;

    [Header("È¹µæ ÈÄ ÄÓ Animator Bool ÀÌ¸§")]
    public string animatorBoolName = "HasLegs";

    [Header("»óÈ£ÀÛ¿ë Å°")]
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
        if (playerInRange && Input.GetKeyDown(interactKey))
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

        if (playerAnimator != null)
        {
            playerAnimator.SetBool(animatorBoolName, true);

            if (GameProgressManager.Instance != null)
            {
                GameProgressManager.Instance.SetHasLegs();
            }
        }
        else
        {
            Debug.LogWarning("Player Animator°¡ ºñ¾îÀÖÀ½");
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerAnimator = other.GetComponent<Animator>();
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