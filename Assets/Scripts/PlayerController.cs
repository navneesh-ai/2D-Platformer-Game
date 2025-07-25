using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float speed = 5f;
    public LayerMask groundLayer; // Assign this in the Inspector to your Ground layer

    public ScoreController scoreController;
    public PlayerHealth playerHealth; // Assign in Inspector
    
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
        float vertical = Input.GetAxis("Vertical");

        // Raycast ground check
        // Cast a short ray down from the center of the player
        float extraHeight = 0.05f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, (originalColliderSize.y / 2f) + extraHeight, groundLayer);
        isGrounded = hit.collider != null;

        PlayerMovement(move, vertical);
        PlayAnimation(move, vertical);       
       
    }

    public void PickUpKey()
    {
        Debug.Log("Player picked up the key");
        scoreController.IncrementScore(10);
    }

    public void KillPlayer()
    {
        Debug.Log("Player killed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            playerHealth.TakeDamage();
        }
    }

    private void PlayerMovement(float move, float vertical)
    {
        // Move the player by changing its transform.position based on speed
        Vector3 position = transform.position;
        position.x += move * speed * Time.deltaTime;
        transform.position = position;

        // Jump logic (movement only)
        if (vertical > 0.5f && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetTrigger("Jump"); // Set jump trigger when jump starts
        }
    }

    private void PlayAnimation(float move, float vertical)
    {
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
}
