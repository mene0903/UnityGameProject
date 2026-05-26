using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("ÇÏÆ® UI")]
    public Image[] hearts;

    [Header("Ã¼·Â")]
    public int maxHealth = 3;

    private int currentHealth;

    void Start()
    {
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
            Debug.Log("ÇÃ·¹ÀÌ¾î »ç¸Á");
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth;
        }
    }
}