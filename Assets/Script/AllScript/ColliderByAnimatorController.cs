using UnityEngine;

public class ColliderByAnimatorController : MonoBehaviour
{
    [System.Serializable]
    public class ColliderSetting
    {
        public RuntimeAnimatorController animatorController;
        public Vector2 colliderSize;
        public Vector2 colliderOffset;
    }

    public Animator animator;
    public BoxCollider2D boxCollider;
    public ColliderSetting[] settings;

    private RuntimeAnimatorController currentController;

    void Start()
    {
        ApplyColliderByController();
    }

    void Update()
    {
        if (animator.runtimeAnimatorController != currentController)
        {
            ApplyColliderByController();
        }
    }

    void ApplyColliderByController()
    {
        currentController = animator.runtimeAnimatorController;

        foreach (ColliderSetting setting in settings)
        {
            if (setting.animatorController == currentController)
            {
                boxCollider.size = setting.colliderSize;
                boxCollider.offset = setting.colliderOffset;
                return;
            }
        }
    }
}