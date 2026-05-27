using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public float interactRange = 2f;  // E키 상호작용 범위
    public float appearRange = 5f;    // 이 범위 안에 들어오면 열쇠 나타남
    public GameObject interactUI;
    public Sprite keySprite;

    private Transform _player;
    private AudioSource _audio;
    private Image _popupImage;
    private bool _collected = false;
    private bool _appeared = false;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _audio = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // 처음엔 스프라이트 숨김 (소리는 나중에 시작)
        _spriteRenderer.enabled = false;
        if (interactUI != null) interactUI.SetActive(false);

        // 팝업 이미지 찾기
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
        if (_collected) return;

        // 플레이어가 사라졌으면 새로 찾기
        if (_player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p == null) return;
            _player = p.transform;
        }

        // 부품 획득 후 소리 시작
        if (GameStateManager.Instance.hasFoundPart && !_audio.isPlaying && !_appeared)
        {
            _audio.Play();
        }

        // 플레이어가 appearRange 안에 들어오면 열쇠 나타남
        if (GameStateManager.Instance.hasFoundPart && !_appeared)
        {
            float dist = Vector2.Distance(transform.position, _player.position);
            if (dist <= appearRange)
            {
                _appeared = true;
                _spriteRenderer.enabled = true;
                _audio.Stop(); // 나타나면 소리 멈춤
            }
        }

        // 열쇠가 나타난 후 E키 상호작용
        if (_appeared)
        {
            float dist = Vector2.Distance(transform.position, _player.position);
            if (dist <= interactRange)
            {
                if (interactUI != null) interactUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                    StartCoroutine(CollectKey());
            }
            else
            {
                if (interactUI != null) interactUI.SetActive(false);
            }
        }
    }

    private System.Collections.IEnumerator CollectKey()
    {
        _collected = true;
        if (interactUI != null) interactUI.SetActive(false);

        // 팝업 이미지 표시
        if (_popupImage != null)
        {
            _popupImage.sprite = keySprite;
            _popupImage.enabled = true;
        }

        yield return new WaitForSeconds(2.5f);

        if (_popupImage != null)
            _popupImage.enabled = false;

        GameStateManager.Instance.hasFoundKey = true;
        gameObject.SetActive(false);
    }
}