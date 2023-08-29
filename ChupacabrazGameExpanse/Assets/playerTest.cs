using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
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
    private LayerMask groundLayerMask;

    private bool isInvulnerable;
    private string currSceneName;

    private bool canTimeTravel;


    public GameObject NotifTxt;
    private Animator camAnimatorRef;

    // "playerHealth", "playerSand", "playerItems", "playerSequenceIndex"

    // Start is called before the first frame update
    void Start()
    {
        currSceneName = SceneManager.GetActiveScene().name;

        NotifTxt = GameObject.Find("TimeTravelTxt");
        NotifTxt.SetActive(false);
        groundLayerMask = LayerMask.GetMask("Ground");
        if (currSceneName == "PresentLevel")
        {
            camAnimatorRef = FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>();
        }
        //camAnimatorRef = FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>();

        canTimeTravel = false;

        GameManager gameManager = GameManager.Instance;

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

        inventory.health = PlayerPrefs.GetInt(gameManager.playerHealth);
        inventory.sand = PlayerPrefs.GetInt(gameManager.playerSand);
        inventory.items = new Item[5];
        for(int i = 0; i < inventory.items.Length; i++)
        {
            inventory.items[i] = emptyShell;
        }
        
        //player init
        isInvulnerable = false;

        //Debug.Log($"{inventory.health}, {inventory.sand}");

    }

    // Update is called once per frame
    void Update()
    {

        //camAnimatorRef.SetBool("Zoomout", false);
        //Debug.Log(inventory.health);

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


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "invisibleBox")
        {
            NotifTxt.SetActive(true);
            if (currSceneName == "PresentLevel")
            {
                camAnimatorRef.SetBool("Zoomout", true);
            }
            canTimeTravel = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "invisibleBox")
        {
            NotifTxt.SetActive(false);
            if (currSceneName == "PresentLevel")
            {
                camAnimatorRef.SetBool("Zoomout", true);
            }
            canTimeTravel = false;
        }
    }





}
