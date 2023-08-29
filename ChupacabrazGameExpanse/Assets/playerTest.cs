using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    //movement and environment variables
    private Rigidbody2D rb;
    private float horizontal;
    private float playerSpeed;
    private float jumpForce;
    private bool isGrounded;
    private float rayCastLength;
    public LayerMask groundLayerMask;

    private bool isInvulnerable;
    private string currSceneName;

    public bool canTimeTravel;


    // Reference variables for unity
    public GameObject NotifTxt;
    public Animator camAnimatorRef;



    // Start is called before the first frame update
    void Start()
    {

        camAnimatorRef = Object.FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>();

        canTimeTravel = false;

        //movement setup
        rb = gameObject.GetComponent<Rigidbody2D>();
        rayCastLength = 0.7f;
        isGrounded = true;
        playerSpeed = 5;
        jumpForce = 10;

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
        
        //player init
        isInvulnerable = false;
        currSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

        //camAnimatorRef.SetBool("Zoomout", false);

        rb.velocity = new Vector2(x: horizontal * playerSpeed, y: rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");

        //jump mechanic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(x: rb.velocity.x, y: jumpForce);
        }

        isGrounded = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: rayCastLength, groundLayerMask);


        //time travelling
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canTimeTravel)
            {
                if (currSceneName == "PresentLevel")
                {
                    SceneManager.LoadScene("FutureLevel");
                    currSceneName = "FutureLevel";
                }
                else if (currSceneName == "FutureLevel")
                {
                    SceneManager.LoadScene("PresentLevel");
                    currSceneName = "PresentLevel";
                }

                Debug.Log(currSceneName);
            }
        }
    }

    public void takeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            inventory.health -= damage;
            if (inventory.health < 0)
            {
                Destroy(gameObject);
            }
            StartCoroutine(invulnerability());
        }
    }
    
    IEnumerator invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(2);
        isInvulnerable = false;
    }

    IEnumerator camAnim()
    {
        camAnimatorRef.SetBool("Zoomout", true);
        yield return new WaitForSeconds(2);
        camAnimatorRef.SetBool("Zoomout", false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "invisibleBox")
        {
            StartCoroutine(camAnim());
            canTimeTravel = true;
            NotifTxt.SetActive(true);
        }
    }




    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "invisibleBox")
        {
            canTimeTravel = false;
            NotifTxt.SetActive(false);
        }
    }

}
