using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratikTest : MonoBehaviour
{
    private int pratikDamage = 1;
    public playerTest pscript;
    public float range = 5f;
    public float speed = 2f;
    public GameObject player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pscript = collision.gameObject.GetComponent<playerTest>();
            pscript.takeDamage(pratikDamage);
        }
    }

    void Start()
    {
        // Assuming the player has a tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (player)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= range)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }
}
