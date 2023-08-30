using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratikMom : MonoBehaviour
{
    public int pratikMomDamage = 1;
    public float bulletVel = 10f;
    public GameObject player;
    public GameObject bult;
    public float momrange = 10f;
    public bool canShoot;
    public Vector2 direction;
    public string posi = "right";
    public Vector2 directionalVelocity;
    public GameObject timelessSandPrefab;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = (player.transform.position - transform.position);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= momrange && canShoot)
        {
            BultThalleKhadiH();
            StartCoroutine(canYouTho());
        }
        if (direction[0] < 0 && posi == "right" || direction[0] > 0 && posi == "left")
        {
            
            transform.Rotate(0, 0, 180);
            posi = posi == "right" ? "left" : "right";
        }
        directionalVelocity = posi == "left" ? new Vector2(-6.78f, -0.23f) : new Vector2(6.78f, -0.23f);
    }

    void BultThalleKhadiH()
    {
        if(player != null)
        {
            GameObject newBult = Instantiate(bult,transform.position,Quaternion.identity);
            direction = (player.transform.position - transform.position);
            newBult.GetComponent<Rigidbody2D>().velocity = Time.deltaTime * bulletVel * directionalVelocity *20;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTest pscript = collision.gameObject.GetComponent<playerTest>();
            pscript.takeDamage(pratikMomDamage);
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

    IEnumerator canYouTho()
    {
        canShoot=false;
        yield return new WaitForSeconds(3);
        canShoot = true;
    }
}
