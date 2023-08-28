using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerTest : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
    {
        public float speed;
        public float jumpHeight;
        public float jumpForce;

    }


    public Vector3 boxSize;
    public float maxDist;


    public LayerMask groundLayer;
    public Transform GroundCheck;
    
    public Stats stats;


    private Rigidbody2D rb;
    public Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
              rb.AddForce(new Vector2(0, -10), ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.velocity = new Vector2(10, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.velocity = new Vector2(-10, 0);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(0, 0);

        }

    }


    private bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDist, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
