using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratikTest : MonoBehaviour
{
    private int pratikDamage = 1;
    public playerTest pscript;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pscript = collision.gameObject.GetComponent<playerTest>();
            pscript.takeDamage(pratikDamage);
        }
    }
}
