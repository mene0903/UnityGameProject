using UnityEngine;

public class ConveyorSpawner : MonoBehaviour
{
    [Header("생성할 오브젝트 프리팹")]
    public GameObject partPrefab;

    [Header("왼쪽 구멍 위치")]
    public Transform spawnPoint;

    [Header("오른쪽 구멍 위치")]
    public Transform destroyPoint;

    [Header("이동 속도")]
    public float moveSpeed = 2f;

    [Header("몇 초마다 생성할지")]
    public float spawnInterval = 3f;

    [Header("생성될 때 회전값")]
    public float spawnRotationZ = 90f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPart), 0f, spawnInterval);
    }

    private void SpawnPart()
    {
        if (partPrefab == null || spawnPoint == null || destroyPoint == null)
        {
            Debug.LogWarning("ConveyorSpawner 설정이 비어있음");
            return;
        }

        Quaternion spawnRotation =
            Quaternion.Euler(0, 0, spawnRotationZ);

        GameObject partObj = Instantiate(
            partPrefab,
            spawnPoint.position,
            spawnRotation
        );

        ConveyorPart part = partObj.GetComponent<ConveyorPart>();

        if (part == null)
        {
            Debug.LogWarning("생성된 부품에 ConveyorPart가 없음");
            return;
        }

        // 현재 ConveyorSpawner 자체를 넘김
        part.SetTarget(destroyPoint, this);
    }
}