using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class BodyInteraction : MonoBehaviour
{
    public float interactRange = 10f;
    public GameObject interactUI;
    public GameObject allCharacterPrefab;
    public Sprite bodySprite;
    public GameObject bgmManagerPrefab;

    private Transform _player;
    private bool _merging = false;
    private Image _popupImage;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        interactUI.SetActive(false);

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
        {
            bot.chaseSpeed = 0f;
            bot.patrolSpeed = 0f;
        }

        if (_popupImage != null)
        {
            _popupImage.sprite = bodySprite;
            _popupImage.enabled = true;
        }

        yield return new WaitForSeconds(2.5f);

        if (_popupImage != null)
            _popupImage.enabled = false;

        foreach (var bot in FindObjectsOfType<RepairBot>())
        {
            bot.chaseSpeed = 1f;
            bot.patrolSpeed = 0.5f;
        }

        if (allCharacterPrefab != null)
        {
            GameObject newCharacter = Instantiate(allCharacterPrefab, _player.position, Quaternion.identity);

            FindBodyPlayerHealth oldHealth = _player.GetComponent<FindBodyPlayerHealth>();
            FindBodyPlayerHealth newHealth = newCharacter.GetComponent<FindBodyPlayerHealth>();

            if (oldHealth != null && newHealth != null)
            {
                // 하트 UI 강제 연결
                newHealth.hearts = new Image[]
                {
                    GameObject.Find("Heart1")?.GetComponent<Image>(),
                    GameObject.Find("Heart2")?.GetComponent<Image>(),
                    GameObject.Find("Heart3")?.GetComponent<Image>()
                };

                // HP 이어받기
                newHealth.SetHealth(oldHealth.GetHealth());
            }
            else
            {
                Debug.LogWarning($"HP 이어받기 실패 - oldHealth: {oldHealth}, newHealth: {newHealth}");
            }
        }

        GameObject globalLightObj = GameObject.Find("Global Light 2D");
        if (globalLightObj != null)
        {
            Light2D globalLight = globalLightObj.GetComponent<Light2D>();
            if (globalLight != null)
                globalLight.intensity = 1f;
        }

        if (bgmManagerPrefab != null)
            Instantiate(bgmManagerPrefab);

        GameStateManager.Instance.hasFoundPart = true;

        Destroy(_player.gameObject);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}