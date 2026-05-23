using UnityEngine;
using TMPro;

public class NumberUINoArm : MonoBehaviour
{
    [Header("번호 Text 4개")]
    public TMP_Text[] numberTexts;

    [Header("정답 순서")]
    public int[] answerOrder = { 3, 2, 1, 4 };

    void Start()
    {
        for (int i = 0; i < numberTexts.Length; i++)
        {
            if (numberTexts[i] != null)
                numberTexts[i].text = "";
        }
    }

    public void ShowNumber(int number)
    {
        for (int i = 0; i < answerOrder.Length; i++)
        {
            if (answerOrder[i] == number)
            {
                numberTexts[i].text = number.ToString();
                return;
            }
        }
    }
}