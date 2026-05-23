using UnityEngine;
using TMPro;
using System.Collections;

public class HiddenNumberItem1 : MonoBehaviour
{
    [Header("이 아이템 번호")]
    public int number = 1;

    [Header("플레이어")]
    public Transform player;

    [Header("반짝임 거리")]
    public float sparkleDistance = 2f;

    [Header("상호작용 거리")]
    public float interactDistance = 1.2f;

    [Header("반짝임 오브젝트")]
    public GameObject sparkleObject;

    [Header("UI")]
    public TMP_Text centerNumberText;
    public NumberUINoArm numberUI;

    private bool collected = false;

    void Start()
    {
        if (player == null)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");

            if (target != null)
                player = target.transform;
        }

        if (sparkleObject != null)
            sparkleObject.SetActive(false);

        if (centerNumberText != null)
            centerNumberText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (collected) return;
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        HandleSparkle(distance);
        HandleInteraction(distance);
    }

    void HandleSparkle(float distance)
    {
        if (sparkleObject == null) return;

        sparkleObject.SetActive(distance <= sparkleDistance);
    }

    void HandleInteraction(float distance)
    {
        if (distance > interactDistance) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }

    void Collect()
    {
        collected = true;

        if (sparkleObject != null)
            sparkleObject.SetActive(false);

        if (numberUI != null)
            numberUI.ShowNumber(number);

        if (GameProgressManager.Instance != null)
            GameProgressManager.Instance.CollectNumber(number);

        StartCoroutine(ShowCenterNumber());

        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
            col.enabled = false;
    }

    IEnumerator ShowCenterNumber()
    {
        if (centerNumberText != null && player != null)
        {
            centerNumberText.text = number.ToString();

            Vector3 screenPos =
                Camera.main.WorldToScreenPoint(
                    player.position + new Vector3(0, 1.5f, 0)
                );

            centerNumberText.transform.position = screenPos;
            centerNumberText.gameObject.SetActive(true);

            yield return new WaitForSeconds(1.2f);

            centerNumberText.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }
}