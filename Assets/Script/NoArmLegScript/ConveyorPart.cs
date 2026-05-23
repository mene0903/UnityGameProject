using UnityEngine;

public class ConveyorPart : MonoBehaviour
{
    private Transform target;

    // ConveyorSpawner 참조
    private ConveyorSpawner conveyorSpawner;

    public void SetTarget(
        Transform targetPoint,
        ConveyorSpawner spawner
    )
    {
        target = targetPoint;
        conveyorSpawner = spawner;
    }

    private void Update()
    {
        if (target == null || conveyorSpawner == null)
            return;

        // 실시간으로 현재 moveSpeed 사용
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            conveyorSpawner.moveSpeed * Time.deltaTime
        );

        // 목적지 도착 시 삭제
        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            Destroy(gameObject);
        }
    }
}