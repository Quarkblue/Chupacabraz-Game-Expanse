using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public string playerSequenceIndex = "playerSequenceIndex", playerHealth = "playerHealth", playerSand = "playerSand", playerItems = "playerItems";

    private void Awake()
    {
        if(_instance != null) Destroy(gameObject);
        else _instance = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData()
    {

        // PlayerPrefs init
        PlayerPrefs.SetInt("playerSequenceIndex", 0);
        PlayerPrefs.SetInt("playerHealth", 5);
        PlayerPrefs.SetInt("playerSand", 0);
        PlayerPrefs.SetInt("mazeRunner", 0);
    }

}
