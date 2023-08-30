using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Animator playerAnim;
    private SpriteRenderer sp;

    public GameManager gameManager;
    public TMPro.TextMeshProUGUI sand, health;


    // "playerHealth", "playerSand", "playerItems", "playerSequenceIndex"

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }


    // Start is called before the first frame update
    void Start()
    {

        sand = GameObject.FindGameObjectWithTag("sandTxt").GetComponent<TextMeshProUGUI>();
        health = GameObject.FindGameObjectWithTag("healthTxt").GetComponent<TextMeshProUGUI>();
        currSceneName = SceneManager.GetActiveScene().name;

        NotifTxt = GameObject.Find("TimeTravelTxt");
        NotifTxt.SetActive(false);
        groundLayerMask = LayerMask.GetMask("Ground");

        playerAnim = gameObject.GetComponent<Animator>();
        sp = gameObject.GetComponent<SpriteRenderer>();

        if (currSceneName == "PresentLevel")
        {
            camAnimatorRef = FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>();
        }
        //camAnimatorRef = FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>();

        canTimeTravel = false;

        GameManager gameManager = GameManager.Instance;

        //movement setup
        rb = gameObject.GetComponent<Rigidbody2D>();
        rayCastLength = 1;
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
        health.text = $"Health: {inventory.health}";
        sand.text = $"Sand: {inventory.sand}";

        //camAnimatorRef.SetBool("Zoomout", false);
        //Debug.Log(inventory.health);

        rb.velocity = new Vector2(x: horizontal * playerSpeed, y: rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");

        if(rb.velocity.x > 0)
        {
            sp.flipX = true;
        }else if(rb.velocity.x < 0)
        {
            sp.flipX = false;
        }

        if(horizontal != 0)
        {
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }


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
            //rb.AddForce(new Vector2(x: -10, y: 0), ForceMode2D.Impulse);
            StartCoroutine(invulnerability());
        }
    }


    IEnumerator invulnerability()
    {
        playerAnim.SetBool("isDamaged", true);
        isInvulnerable = true;
        yield return new WaitForSeconds(2);
        isInvulnerable = false;
        playerAnim.SetBool("isDamaged", false);

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "portal")
        {
            PresentGameController presentGameController = FindObjectOfType<PresentGameController>();
            presentGameController.toMaze();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "invisibleBox")
        {
            NotifTxt.SetActive(true);
            if (currSceneName == "PresentLevel" && PlayerPrefs.GetInt(gameManager.playerSequenceIndex) == 0)
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
            if (currSceneName == "PresentLevel" && PlayerPrefs.GetInt(gameManager.playerSequenceIndex) == 0)
            {
                camAnimatorRef.SetBool("Zoomout", true);
            }
            canTimeTravel = false;
        }
    }





}
