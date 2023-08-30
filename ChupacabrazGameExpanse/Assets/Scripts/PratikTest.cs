using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratikTest : MonoBehaviour
{
    private int pratikDamage = 1;
    public playerTest pscript;
    public float range = 10f;
    public float speed = 2f;
    public GameObject player;
    public GameObject timelessSandPrefab;
    

    void Start()
    {
        // Assuming the player has a tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            float distancep = Vector3.Distance(transform.position, player.transform.position);
            if (distancep <= range)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pscript = collision.gameObject.GetComponent<playerTest>();
            pscript.takeDamage(pratikDamage);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Die();
        }
    }

    public void Die()
    {
        //Instantiate(timelessSandPrefab, transform.position, Quaternion.identity);
        //timelessSandPrefab.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        Destroy(gameObject);
    }

}
