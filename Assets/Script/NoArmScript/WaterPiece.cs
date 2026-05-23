using UnityEngine;

public class WaterPiece : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 moveDirection = Vector2.right;
    public float lifeTime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ChemicalPuddle"))
        {
            Destroy(collision.gameObject);
        }
    }
}