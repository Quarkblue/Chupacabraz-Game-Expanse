using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndSwitchScreen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitAndSwitchScreen()
    {
        yield return new WaitForSeconds(7.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
    }

}
