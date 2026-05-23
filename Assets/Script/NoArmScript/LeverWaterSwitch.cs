using UnityEngine;
using System.Collections;

public class LeverWaterSwitch : MonoBehaviour
{
    [Header("레버 스프라이트")]
    public Sprite leverOffSprite;
    public Sprite leverOnSprite;

    [Header("물 프리팹")]
    public GameObject waterPrefab;

    [Header("물 생성 위치")]
    public Transform spawnPoint;

    [Header("물 생성 설정")]
    public float spawnInterval = 0.1f;
    public float flowDuration = 2f;

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

            StartCoroutine(SpawnWaterFlow());
        }
    }

    private IEnumerator SpawnWaterFlow()
    {
        float timer = 0f;

        while (timer < flowDuration)
        {
            Instantiate(
                waterPrefab,
                spawnPoint.position,
                Quaternion.identity
            );

            yield return new WaitForSeconds(spawnInterval);

            timer += spawnInterval;
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