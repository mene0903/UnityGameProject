using UnityEngine;
using UnityEngine.UI;

public class AllPlayerHealth : MonoBehaviour
{
    [Header("하트 UI")]
    public Image[] hearts;

    [Header("체력")]
    public int maxHealth = 3;

    private int currentHealth;

    void Start()
    {
        // 혹시 인스펙터에 안 넣었으면 자동으로 찾기
        if (hearts == null || hearts.Length == 0 || hearts[0] == null)
        {
            hearts = new Image[]
            {
                GameObject.Find("Heart1")?.GetComponent<Image>(),
                GameObject.Find("Heart2")?.GetComponent<Image>(),
                GameObject.Find("Heart3")?.GetComponent<Image>()
            };
        }

        currentHealth = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage()
    {
        currentHealth--;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHearts();

        if (currentHealth <= 0)
        {
            Debug.Log("플레이어 사망");
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].enabled = i < currentHealth;
            }
            else
            {
                Debug.LogWarning("하트 " + i + "번이 연결 안 됨");
            }
        }
    }
}