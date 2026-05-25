using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BodyInteraction : MonoBehaviour
{
    public float interactRange = 1.5f;
    public GameObject interactUI;
    public GameObject allCharacterPrefab;

    private Transform _player;
    private bool _merging = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        interactUI.SetActive(false);
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

        // 모든 RepairBot 멈추기
        foreach (var bot in FindObjectsOfType<RepairBot>())
            bot.Freeze();

        yield return new WaitForSeconds(0.3f);

        // AllCharacter 스폰
        if (allCharacterPrefab != null)
            Instantiate(allCharacterPrefab, transform.position, Quaternion.identity);

        // 이름으로 Global Light 2D 찾아서 켜기
        GameObject globalLightObj = GameObject.Find("Global Light 2D");
        if (globalLightObj != null)
        {
            Light2D globalLight = globalLightObj.GetComponent<Light2D>();
            if (globalLight != null)
                globalLight.intensity = 1f;
        }

        // Face와 body_0 제거
        Destroy(_player.gameObject);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}