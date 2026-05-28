using UnityEngine;
using UnityEngine.UI;

public class DebugKillPlayer : MonoBehaviour
{
    public Image[] hearts;

    void Update()
    {
        // 한글 'ㅡ' 위치 = M 키
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (Image heart in hearts)
            {
                if (heart != null)
                    heart.enabled = false;
            }

            Debug.Log("하트 전부 제거됨");
        }
    }
}