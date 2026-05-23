using UnityEngine;

public class ConveyorSpeedController : MonoBehaviour
{
    [Header("컨베이어 매니저")]
    public ConveyorSpawner conveyorSpawner;

    [Header("속도 감소량")]
    public float decreaseAmount = 1f;

    [Header("최소 속도")]
    public float minSpeed = 1f;

    private bool isPlayerNear = false;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            DecreaseSpeed();
        }
    }

    private void DecreaseSpeed()
    {
        if (conveyorSpawner == null)
        {
            Debug.LogWarning("ConveyorSpawner 연결 안됨");
            return;
        }

        conveyorSpawner.moveSpeed -= decreaseAmount;

        // 최소 속도 제한
        if (conveyorSpawner.moveSpeed < minSpeed)
        {
            conveyorSpawner.moveSpeed = minSpeed;
        }

        Debug.Log("현재 컨베이어 속도 : " + conveyorSpawner.moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}