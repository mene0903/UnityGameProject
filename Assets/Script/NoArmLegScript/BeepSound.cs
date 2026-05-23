using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
    [Header("Settings")]
    public Transform player;          // 플레이어 (직접 할당하거나 코드에서 찾기)
    public AudioSource beepSource;    // 사용할 AudioSource
    public float maxDistance = 15f;   // 감지 시작 거리

    [Header("Interval (Speed)")]
    public float minInterval = 0.1f;  // 가장 가까울 때 소리 간격 (매우 빠름)
    public float maxInterval = 1.2f;  // 가장 멀 때 소리 간격 (느림)

    [Header("Volume Control")]
    public float minVolume = 0.1f;    // 멀리 있을 때의 최소 볼륨
    public float maxVolume = 1.0f;    // 아주 가까울 때의 최대 볼륨

    private float timer;

    void Update()
    {
        if (player == null) return;

        // 1. 거리 계산
        float distance = Vector3.Distance(transform.position, player.position);

        // 2. 최대 거리 이내일 때만 작동
        if (distance <= maxDistance)
        {
            // 3. 거리를 0(가까움)에서 1(멂) 사이의 비율로 변환
            float distanceRatio = Mathf.Clamp01(distance / maxDistance);

            // 4. 비율에 따라 볼륨 조절 (가까울수록 커짐)
            beepSource.volume = Mathf.Lerp(maxVolume, minVolume, distanceRatio);

            // 5. 비율에 따라 간격 조절 (가까울수록 짧아짐)
            float currentInterval = Mathf.Lerp(minInterval, maxInterval, distanceRatio);

            // 6. 타이머 로직으로 소리 재생
            timer += Time.deltaTime;
            if (timer >= currentInterval)
            {
                beepSource.PlayOneShot(beepSource.clip);
                timer = 0f;
            }
        }
        else
        {
            // 범위를 벗어나면 타이머 초기화 (다시 들어왔을 때 즉시 소리가 나게 하려면)
            timer = maxInterval;
        }
    }
}