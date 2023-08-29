using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentGameController : MonoBehaviour
{

    public GameObject SideASpawn, SideBSpawn;
    public GameObject playerPrefab;
    public GameManager gameManager;

    private Cinemachine.CinemachineVirtualCamera cam;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(gameManager.playerSequenceIndex) == 0)
        {
            cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
            Instantiate(playerPrefab, SideASpawn.transform.position, Quaternion.identity);
            cam.Follow = GameObject.Find(GameObject.Find("Player(Clone)").name).transform;
        }
        else if(PlayerPrefs.GetInt(gameManager.playerSequenceIndex) == 1)
        {
            cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
            Instantiate(playerPrefab, SideBSpawn.transform.position, Quaternion.identity);
            cam.Follow = GameObject.Find(GameObject.Find("Player(Clone)").name).transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadPlayerData()
    {

    }


}
