using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class BodyInteraction : MonoBehaviour
{
    public float interactRange = 10f;
    public GameObject interactUI;
    public GameObject allCharacterPrefab;
    public Sprite bodySprite;

    private Transform _player;
    private bool _merging = false;
    private Image _popupImage;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        interactUI.SetActive(false);

        // AllCanvas에서 직접 찾기
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            Image[] images = canvas.GetComponentsInChildren<Image>(true);
            foreach (var img in images)
            {
                if (img.gameObject.name == "ItemPopupImage")
                {
                    _popupImage = img;
                    break;
                }
            }
        }

        Debug.Log("팝업 이미지 찾음: " + _popupImage);

        if (_popupImage != null)
            _popupImage.enabled = false;
    }

    void Update()
    {
        if (_merging) return;
        float dist = Vector2.Distance(transform.position, _player.position);
        if (dist <= interactRange)
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
                StartCoroutine(MergeSequence());
        }
        else
        {
            interactUI.SetActive(false);
        }
    }

    private System.Collections.IEnumerator MergeSequence()
    {
        _merging = true;
        interactUI.SetActive(false);

        foreach (var bot in FindObjectsOfType<RepairBot>())
            bot.Freeze();

        // 팝업 이미지 표시
        if (_popupImage != null)
        {
            _popupImage.sprite = bodySprite;
            _popupImage.enabled = true;
        }

        yield return new WaitForSeconds(2.5f);

        if (_popupImage != null)
            _popupImage.enabled = false;

        

        if (allCharacterPrefab != null)
            Instantiate(allCharacterPrefab, _player.position, Quaternion.identity);

        GameObject globalLightObj = GameObject.Find("Global Light 2D");
        if (globalLightObj != null)
        {
            Light2D globalLight = globalLightObj.GetComponent<Light2D>();
            if (globalLight != null)
                globalLight.intensity = 1f;
            GameStateManager.Instance.hasFoundPart = true;
        }

        Destroy(_player.gameObject);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}