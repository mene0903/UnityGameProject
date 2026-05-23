    using UnityEngine;
    using TMPro;

    public class NumberUI : MonoBehaviour
    {
        [Header("¹øÈ£ Text 4°³")]
        public TMP_Text[] numberTexts;

        void Start()
        {
            for (int i = 0; i < numberTexts.Length; i++)
            {
                numberTexts[i].text = "";
            }
        }

        public void ShowNumber(int number)
        {
            int index = number - 1;

            if (index < 0 || index >= numberTexts.Length)
                return;

            numberTexts[index].text = number.ToString();
        }
    }