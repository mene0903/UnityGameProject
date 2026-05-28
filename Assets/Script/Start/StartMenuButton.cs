using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButton : MonoBehaviour
{
    [Header("이동할 씬 이름")]
    public string sceneName;

    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            if (col.OverlapPoint(worldPos))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}