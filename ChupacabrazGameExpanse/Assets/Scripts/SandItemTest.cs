using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandItemTest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTest pscript = collision.gameObject.GetComponent<playerTest>();
            if (pscript != null && pscript.inventory.sand < 20)
            {
                pscript.inventory.sand = pscript.inventory.sand + 1;
                Destroy(gameObject);
            }
            
        }
    }
}
