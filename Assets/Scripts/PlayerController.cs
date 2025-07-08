using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(move));

        // Flip the player based on movement direction
        if (move > 0.01f)
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        else if (move < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
        }
}
