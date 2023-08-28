using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryTest : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject InventoryCanvas;
    void Start()
    {
        InventoryCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryCanvas.SetActive(!InventoryCanvas.activeSelf);
        }
    }
}
