using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    [Header("´Ù¸® È¹µæ ¿©ºÎ")]
    public bool hasLegs = false;

    [Header("¹øÈ£ È¹µæ ¿©ºÎ")]
    public bool[] collectedNumbers = new bool[4];

    void Awake()
    {
        Instance = this;
    }

    public void SetHasLegs()
    {
        hasLegs = true;
        Debug.Log("´Ù¸® È¹µæ ¿Ï·á");
    }

    public void CollectNumber(int number)
    {
        int index = number - 1;

        if (index < 0 || index >= collectedNumbers.Length)
            return;

        collectedNumbers[index] = true;
    }

    public bool HasAllNumbers()
    {
        for (int i = 0; i < collectedNumbers.Length; i++)
        {
            if (!collectedNumbers[i])
                return false;
        }

        return true;
    }

    public bool CanOpenNextDoor()
    {
        return hasLegs && HasAllNumbers();
    }
}   