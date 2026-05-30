using UnityEngine;

public class CharacterStateChanger : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCollider;

    public RuntimeAnimatorController headOnlyController;
    public RuntimeAnimatorController bodyController;
    public RuntimeAnimatorController legController;

    public Vector2 headOnlySize;
    public Vector2 headOnlyOffset;

    public Vector2 bodySize;
    public Vector2 bodyOffset;

    public Vector2 legSize;
    public Vector2 legOffset;

    public void ChangeToHeadOnly()
    {
        animator.runtimeAnimatorController = headOnlyController;
        boxCollider.size = headOnlySize;
        boxCollider.offset = headOnlyOffset;
    }

    public void ChangeToBody()
    {
        animator.runtimeAnimatorController = bodyController;
        boxCollider.size = bodySize;
        boxCollider.offset = bodyOffset;
    }

    public void ChangeToLeg()
    {
        animator.runtimeAnimatorController = legController;
        boxCollider.size = legSize;
        boxCollider.offset = legOffset;
    }
}