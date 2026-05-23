using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NumberInputUI : MonoBehaviour
{
    [Header("입력창")]
    public TMP_InputField inputField;

    [Header("결과 텍스트")]
    public TMP_Text resultText;

    [Header("정답")]
    public string correctCode = "1234";

    [Header("다음 씬 이름")]
    public string nextSceneName;

    void Start()
    {
        resultText.text = "";
    }

    public void CheckCode()
    {
        if (inputField.text == correctCode)
        {
            resultText.color = Color.green;
            resultText.text = "문이 열렸습니다.";

            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            resultText.color = Color.red;
            resultText.text = "번호가 틀렸습니다.";
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}