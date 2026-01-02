using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5.0f;

    private float horizontal;
    private float vertical;

    public Vector2 Direction { get; private set; }

    private Rigidbody2D rb;
    private PlayerAnimation playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Direction = new Vector2(horizontal, vertical).normalized;
        playerAnimation.UpdateAnimation(Direction);
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + (Direction * speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }
}
