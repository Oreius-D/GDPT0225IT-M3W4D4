using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    private Vector2 lastDir = Vector2.down;

    public void UpdateAnimation(Vector2 direction)
    {
        bool isMoving = direction.sqrMagnitude > 0.01f;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
            lastDir = direction;

        // flip
        if (lastDir.x > 0.01f) sr.flipX = false;
        else if (lastDir.x < -0.01f) sr.flipX = true;

        // se nel blend tree hai solo side a (1,0)
        float x = Mathf.Abs(lastDir.x);
        float y = lastDir.y;

        // snap 4 direzioni (consigliato per coerenza)
        if (x > Mathf.Abs(y))
        {
            y = 0f; x = 1f;
        }
        else
        {
            x = 0f; y = Mathf.Sign(y);
        }

        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }
}