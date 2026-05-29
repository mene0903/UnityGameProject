using UnityEngine;
using UnityEngine.UI;
public class FindBodyPlayerHealth : MonoBehaviour
{
    [Header("하트 UI")]
    public Image[] hearts;
    [Header("체력")]
    public int maxHealth = 3;
    public int currentHealth;
    void Start()
    {
        if (hearts == null || hearts.Length == 0 || hearts[0] == null)
        {
            hearts = new Image[]
            {
                GameObject.Find("Heart1")?.GetComponent<Image>(),
                GameObject.Find("Heart2")?.GetComponent<Image>(),
                GameObject.Find("Heart3")?.GetComponent<Image>()
            };
        }
        if (currentHealth == 0)
            currentHealth = maxHealth;
        UpdateHearts();
    }
    public void SetHealth(int health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        UpdateHearts();
    }
    public int GetHealth()
    {
        return currentHealth;
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
                hearts[i].enabled = i < currentHealth;
        }
    }
}