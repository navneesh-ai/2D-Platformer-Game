using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float jumpForce = 5f;
    private bool isGrounded = true; // Simple grounded check for demo
    private BoxCollider2D boxCollider;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    public float crouchHeightMultiplier = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(move));

        // MODIFY LOGIC SO THAT WE DON'T HARD CODE THE SCALE, INSTEAD REVERSE THE DIRECTION OF THE X COMPONENT
        if (move > 0.01f)
        {
            Vector3 scale = transform.localScale;
            if (scale.x < 0)
                scale.x = -scale.x;
            transform.localScale = scale;
        }
        else if (move < -0.01f)
        {
            Vector3 scale = transform.localScale;
            if (scale.x > 0)
                scale.x = -scale.x;
            transform.localScale = scale;
        }

        // Jump logic
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetTrigger("Jump");
        }

        // Crouch logic
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            animator.SetBool("Crouch", true);
            // Adjust collider size for crouch
            boxCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y * crouchHeightMultiplier);
            boxCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y * (1 - crouchHeightMultiplier) / 2f));
        }
        else
        {
            animator.SetBool("Crouch", false);
            // Restore collider size
            boxCollider.size = originalColliderSize;
            boxCollider.offset = originalColliderOffset;
        }
    }

    // Simple ground check (replace with your own collision logic)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
