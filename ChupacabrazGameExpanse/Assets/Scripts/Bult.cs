using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bult : MonoBehaviour
{
    public int bultDmg = 3;

    void Start()
    {
        StartCoroutine(killall());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTest pscript = collision.gameObject.GetComponent<playerTest>();
            pscript.takeDamage(bultDmg);
            Destroy(gameObject);
        }
    }
    
    IEnumerator killall()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
