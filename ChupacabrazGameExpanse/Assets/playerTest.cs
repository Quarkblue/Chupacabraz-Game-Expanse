using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerTest : MonoBehaviour
{
    //Inventory management

    public struct Item
    {
        public int id;
        public GameObject thumbnail;
        public string name;
    }
    public Item emptyShell;
    public struct Inventory
    {
        public int health, sand;
        public Item[] items;
    }
    public Inventory inventory;

    //movement variables
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
        //movement setup
        rb = gameObject.GetComponent<Rigidbody2D>();
        rayCastLength = 0.7f;
        isGrounded = true;
        playerSpeed = 5;
        jumpForce = 7;

        //inventory init
        emptyShell.id = 0;
        emptyShell.thumbnail = null;
        emptyShell.name = null;

        inventory.health = 5;
        inventory.sand = 0;
        inventory.items = new Item[5];
        for(int i = 0; i < inventory.items.Length; i++)
        {
            inventory.items[i] = emptyShell;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(x: horizontal * playerSpeed, y: rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");

        //jump mechanic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(x: rb.velocity.x, y: jumpForce);
        }

        isGrounded = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: rayCastLength, groundLayerMask);

    }

}
