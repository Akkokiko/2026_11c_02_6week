
using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateMovement(bool isMoving)
    {
        if (animator != null)
            animator.SetBool("IsMoving", isMoving);
    }

    public void FaceDirection(float direction)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = direction > 0;
    }
}
