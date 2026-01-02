using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    private Vector2 lastDir = Vector2.down;

    public void UpdateAnimation(Vector2 direction)
    {
        bool isMoving = direction.sqrMagnitude > 0.001f;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
            lastDir = direction;

        // 1) Flip: sinistra/destra
        if (lastDir.x > 0.01f) sr.flipX = false;
        else if (lastDir.x < -0.01f) sr.flipX = true;

        // 2) Se nel Blend Tree hai SOLO Side a (1,0), allora X deve essere sempre positivo
        float x = Mathf.Abs(lastDir.x);
        float y = lastDir.y;

        // 3) (Consigliato) Snap a 4 direzioni per evitare gballih in diagonale
        if (x > Mathf.Abs(y))
        {
            y = 0f;
            x = 1f; // side
        }
        else
        {
            x = 0f;
            y = Mathf.Sign(y); // up/down
        }

        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }
}
