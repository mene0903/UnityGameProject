using UnityEngine;

public class ConveyorPart : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;

    // ConveyorSpawner가 호출하는 함수
    public void SetTarget(Transform newTarget, float newMoveSpeed)
    {
        target = newTarget;
        moveSpeed = newMoveSpeed;
    }

    private void Update()
    {
        if (target == null) return;

        // 목표 지점까지 이동
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        // 목표 지점 도착 시 삭제
        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            Destroy(gameObject);
        }
    }
}