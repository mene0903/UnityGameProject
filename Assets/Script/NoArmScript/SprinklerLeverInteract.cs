using UnityEngine;

public class SprinklerLeverInteract : MonoBehaviour
{
    [Header("레버 이미지")]
    public Sprite leverOffSprite;
    public Sprite leverOnSprite;
    public SpriteRenderer leverRenderer;

    [Header("스프링클러 컨트롤러")]
    public SprinklerLeverController sprinklerController;

    [Header("상호작용")]
    public string playerTag = "Player";
    public KeyCode interactKey = KeyCode.E;

    private bool playerInRange = false;
    private bool activated = false;

    void Reset()
    {
        leverRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!playerInRange) return;
        if (activated) return;

        if (Input.GetKeyDown(interactKey))
        {
            activated = true;

            if (leverRenderer != null && leverOnSprite != null)
                leverRenderer.sprite = leverOnSprite;

            if (sprinklerController != null)
                sprinklerController.ActivateSprinklers();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            playerInRange = false;
    }
}