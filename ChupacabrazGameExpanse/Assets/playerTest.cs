using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerTest : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float playerSpeed;
    private float jumpForce;
    private bool isGrounded;
    private float rayCastLength;
    public LayerMask groundLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rayCastLength = 0.7f;
        isGrounded = true;
        playerSpeed = 5;
        jumpForce = 7;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(x: horizontal * playerSpeed, y: rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");
        //Debug.Log(horizontal);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(x: rb.velocity.x, y: jumpForce);
        }

        isGrounded = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: rayCastLength, groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.down * rayCastLength, Color.red);
    }

}
