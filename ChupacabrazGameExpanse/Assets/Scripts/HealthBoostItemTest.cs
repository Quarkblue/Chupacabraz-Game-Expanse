using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBoostItemTest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTest pscript = collision.gameObject.GetComponent<playerTest>();
            if (pscript != null && pscript.inventory.health < 100)
            {
                pscript.inventory.health = pscript.inventory.health + 1;
                //PlayerPrefs.SetInt("playerHealth", PlayerPrefs.GetInt("playerHealth") + 1);
                Destroy(gameObject);
            }
        }
    }
}
