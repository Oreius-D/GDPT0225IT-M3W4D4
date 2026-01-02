using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PlayerController player;

    Rigidbody2D rb;
    EnemyAnimation enemyAnimation;

    public Vector2 Direction { get; private set; }

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        // se è sullo stesso GO e ti scordi di trascinarlo
        if (enemyAnimation == null)
            enemyAnimation = GetComponent<EnemyAnimation>();
    }

    void Update()
    {
        if (player == null)
        {
            Direction = Vector2.zero;
            if (rb != null) rb.velocity = Vector2.zero;

            if (enemyAnimation != null)
                enemyAnimation.UpdateAnimation(Direction);

            return;
        }

        EnemyMovement();

        if (enemyAnimation != null)
            enemyAnimation.UpdateAnimation(Direction);
    }

    void EnemyMovement()
    {
        Vector2 enemyPos = rb != null ? rb.position : (Vector2)transform.position;
        Vector2 playerPos = player.transform.position;

        Vector2 toPlayer = (playerPos - enemyPos);
        Direction = toPlayer.sqrMagnitude > 0.0001f ? toPlayer.normalized : Vector2.zero;

        Vector2 newPos = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);

        if (rb != null) rb.MovePosition(newPos);
        else transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            Destroy(p.gameObject);
            player = null;

            Direction = Vector2.zero;
            if (rb != null) rb.velocity = Vector2.zero;

            if (enemyAnimation != null)
                enemyAnimation.UpdateAnimation(Direction);
        }
    }
}
