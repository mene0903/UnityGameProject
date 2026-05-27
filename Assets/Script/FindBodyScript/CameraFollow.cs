using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Forever_ChaseCamera : MonoBehaviour
{
    [Header("카메라 이동 제한 범위")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    void LateUpdate()
    {
        Vector3 pos = this.transform.position;

        // 맵 경계 밖으로 카메라 나가지 않도록 제한
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = -10;

        Camera.main.gameObject.transform.position = pos;
    }
}