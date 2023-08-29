using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureSceneController : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject SideASpawn;
    private Cinemachine.CinemachineVirtualCamera cam;
    public GameObject PlayerPrefab;


    private void Awake()
    {
        gameManager = GameManager.Instance;
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt(gameManager.playerSequenceIndex, 1);
        cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        Instantiate(PlayerPrefab, SideASpawn.transform.position, Quaternion.identity);
        cam.Follow = GameObject.Find(GameObject.Find("Player(Clone)").name).transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
