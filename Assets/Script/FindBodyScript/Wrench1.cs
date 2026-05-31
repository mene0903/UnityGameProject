using UnityEngine;

public class Wrench1 : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;

    private float progress = 0f;
    private float speed = 1.5f;
    private float baseSize = 0.3f;

    public void Launch(Vector3 from, Vector3 to)
    {
        startPos = from;
        endPos = to;

        transform.position = from;
        progress = 0f;
    }

    void Update()
    {
        progress += Time.deltaTime * speed;

        Vector3 pos = Vector3.Lerp(startPos, endPos, progress);
        transform.position = pos;

        float scaleMult =
            1f + 0.3f * Mathf.Sin(Mathf.PI * progress);

        transform.localScale = new Vector3(
            baseSize * scaleMult,
            baseSize * scaleMult,
            1f);

        transform.Rotate(0, 0, 360 * Time.deltaTime);

        if (progress >= 1f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerHealth ph =
            other.GetComponentInParent<PlayerHealth>();

        if (ph != null)
        {
            ph.TakeDamage();
            Debug.Log("플레이어 데미지!");
        }

        Destroy(gameObject);
    }
}