using UnityEngine;
using UnityEngine.Rendering.Universal;
public class KeyEffect : MonoBehaviour
{
    [Header("둥둥 효과")]
    public float floatSpeed = 1.5f;
    public float floatHeight = 0.2f;
    [Header("회전 효과")]
    public float rotateSpeed = 90f;
    private Vector3 startPos;
    private Light2D _light;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        startPos = transform.position;
        _light = GetComponent<Light2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // 처음엔 빛 끄기
        if (_light != null)
            _light.enabled = false;
    }
    void Update()
    {
        // 스프라이트가 보일 때만 효과 활성화
        if (_spriteRenderer != null && !_spriteRenderer.enabled) return;
        // 빛 켜기
        if (_light != null && !_light.enabled)
            _light.enabled = true;
        // 둥둥 떠다니기
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        // 회전
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}