using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float jumpForce = 5f;
    private bool isGrounded = true; // Simple grounded check for demo
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
            // Optionally, adjust collider size here
        }
        else
        {
            animator.SetBool("Crouch", false);
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
