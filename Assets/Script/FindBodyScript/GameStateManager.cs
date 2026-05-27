using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public bool hasFoundPart = false;  // ºÎÇ° È¹µæ ¿©ºÎ
    public bool hasFoundKey = false;   // ¿­¼è È¹µæ ¿©ºÎ

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}