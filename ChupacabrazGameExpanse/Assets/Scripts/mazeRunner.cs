using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeRunner : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Adjust the speed as needed

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private float timeRemaining;

    public TMPro.TextMeshProUGUI timerTxt;

    private void Start()
    {
        PlayerPrefs.SetFloat("timeRemaining", 100);

        timeRemaining = PlayerPrefs.GetFloat("timeRemaining");

        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f);

        // Normalize the movement vector to prevent diagonal movement being faster
        movement.Normalize();

        // Move the character using the Rigidbody2D component
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = movement * moveSpeed;

        //if (movement.x > 0)
        //{
        //    spriteRenderer.flipY = false; // Face right
        //    gameObject.transform.Rotate(new Vector3(0, 0, -90)); // Face right
        //}
        //else if (movement.x < 0)
        //{
        //    spriteRenderer.flipY = true; // Face left
        //}

        //// Flip the sprite based on movement direction along the Y-axis
        //if (movement.y > 0)
        //{
        //    spriteRenderer.flipY = false; // Face up
        //    gameObject.transform.Rotate(new Vector3(0, 0, 90)); // Face right
        //}
        //else if (movement.y < 0)
        //{
        //    spriteRenderer.flipY = true; // Face down
        //}

        if (timeRemaining > 0)
        {
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);

            timeRemaining = timeRemaining - Time.deltaTime;
            timerTxt.text = $"Timer: {minutes}:{seconds}";
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PresentLevel");
        }



    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            PlayerPrefs.SetInt("mazeRunner", 1);
            UnityEngine.SceneManagement.SceneManager.LoadScene("PresentLevel");
        }
    }


}
