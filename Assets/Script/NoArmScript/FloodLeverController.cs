using UnityEngine;

public class FloodLeverController : MonoBehaviour
{
    [Header("정화 시스템")]
    public FloodCleaner floodCleaner;

    [Header("플레이어")]
    public Transform player;

    [Header("상호작용 키")]
    public KeyCode interactKey = KeyCode.E;

    [Header("상호작용 거리")]
    public float interactDistance = 1.5f;

    [Header("레버 스프라이트")]
    public Sprite leverOffSprite;
    public Sprite leverOnSprite;

    private bool isPulled = false;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (sr != null && leverOffSprite != null)
            sr.sprite = leverOffSprite;
    }

    void Update()
    {
        if (isPulled) return;
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= interactDistance && Input.GetKeyDown(interactKey))
        {
            isPulled = true;

            if (sr != null && leverOnSprite != null)
                sr.sprite = leverOnSprite;

            if (floodCleaner != null)
                floodCleaner.StartFloodClean();
        }
    }
}