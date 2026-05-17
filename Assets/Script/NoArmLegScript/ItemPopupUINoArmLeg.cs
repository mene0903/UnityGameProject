using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopupUI : MonoBehaviour
{
    public Image popupImage;
    public float showTime = 2f;

    Coroutine popupCoroutine;

    void Start()
    {
        popupImage.enabled = false;
    }

    public void ShowItem(Sprite itemSprite)
    {
        if (popupCoroutine != null)
        {
            StopCoroutine(popupCoroutine);
        }

        popupCoroutine =
            StartCoroutine(ShowItemCoroutine(itemSprite));
    }

    IEnumerator ShowItemCoroutine(Sprite itemSprite)
    {
        popupImage.sprite = itemSprite;

        popupImage.enabled = true;

        yield return new WaitForSeconds(showTime);

        popupImage.enabled = false;
    }
}