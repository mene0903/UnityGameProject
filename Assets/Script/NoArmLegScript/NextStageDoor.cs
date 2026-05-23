using UnityEngine;

public class NextStageDoor : MonoBehaviour
{
    [Header("플레이어")]
    public Transform player;

    [Header("상호작용 거리")]
    public float interactDistance = 1.5f;

    [Header("번호 입력 UI")]
    public GameObject numberInputPanel;

    void Start()
    {
        if (player == null)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");

            if (target != null)
                player = target.transform;
        }

        if (numberInputPanel != null)
            numberInputPanel.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= interactDistance && Input.GetKeyDown(KeyCode.E))
        {
            TryOpenNumberPanel();
        }
    }

    void TryOpenNumberPanel()
    {
        if (GameProgressManager.Instance == null)
            return;

        if (GameProgressManager.Instance.CanOpenNextDoor())
        {
            if (numberInputPanel != null)
                numberInputPanel.SetActive(true);
        }
        else
        {
            Debug.Log("조건 부족: 다리 부품과 번호 4개가 모두 필요함");
        }
    }
}