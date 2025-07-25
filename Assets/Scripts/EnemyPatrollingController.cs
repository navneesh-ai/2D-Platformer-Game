using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    [Tooltip("If true, enemy starts moving right. If false, starts moving left.")]
    public bool moveRight = true;

    private Rigidbody2D rb;
    private int moveDirection = 1; // 1 for right, -1 for left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = moveRight ? 1 : -1;
        // Ensure the sprite is facing the correct direction at start
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (moveRight ? 1 : -1);
        transform.localScale = scale;
        // To prevent enemies from colliding with each other, assign them to a unique layer (e.g., "Enemy")
        // and disable collision between that layer in Unity's Layer Collision Matrix.
    }

    void Update()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TurnAround"))
        {
            moveDirection *= -1;
            // Flip the sprite to face the new direction
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (moveDirection > 0 ? 1 : -1);
            transform.localScale = scale;
        }
    }
}
