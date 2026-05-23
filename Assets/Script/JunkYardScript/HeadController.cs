using UnityEngine;

public class HeadController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotateSpeed = 200f;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        transform.position += new Vector3(moveX * moveSpeed * Time.deltaTime, moveY * moveSpeed * Time.deltaTime, 0);

        transform.Rotate(0, 0, (-moveX + moveY) * rotateSpeed * Time.deltaTime);
    }
}