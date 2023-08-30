using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentGameController : MonoBehaviour
{

    public GameObject SideASpawn, SideBSpawn;
    public GameObject playerPrefab;
    public GameManager gameManager;
    public TextMeshProUGUI timerTxt;
    public GameObject currObjective;
    public GameObject portal;
    public GameObject animScreen;
    public GameObject preMazeTxt;
    public GameObject PostMazeTxt;
    public GameObject bridge;


    public float timeRemaining = 120;


    private Cinemachine.CinemachineVirtualCamera cam;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        animScreen.SetActive(false);
        preMazeTxt.SetActive(false);

        if(PlayerPrefs.GetInt("mazeRunner") == 1)
        {
            portal.SetActive(false);
            bridge.SetActive(true);
        }
        else
        {
            portal.SetActive(true);
            bridge.SetActive(false);
        }

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

        timerTxt.gameObject.SetActive(false);

        //if (PlayerPrefs.GetInt("mazeRunner") == 1)
        //{
        //    animScreen.SetActive(false);
        //    preMazeTxt.SetActive(false);
        //}

        if (PlayerPrefs.GetInt("mazeRunner") == 1)
        {
            StartCoroutine(waitAndAnimAfterMaze());

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt(gameManager.playerSequenceIndex) == 1)
        {
            currObjective.SetActive(true);
            timerTxt.gameObject.SetActive(true);
            if (timeRemaining > 0)
            {
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);

                timeRemaining = timeRemaining - Time.deltaTime;
                timerTxt.text = $"Timer: {minutes}:{seconds}";
            }
            else
            {
                // lost
            }
        }
        

    }

    IEnumerator waitAndAnimToMaze()
    {
        animScreen.SetActive(true);
        preMazeTxt.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Maze");
    }

    IEnumerator waitAndAnimAfterMaze()
    {
        timerTxt.gameObject.SetActive(false);
        currObjective.SetActive(true);
        animScreen.SetActive(true);
        PostMazeTxt.SetActive(true);
        yield return new WaitForSeconds(3);
        timerTxt.gameObject.SetActive(false);
        currObjective.SetActive(false);
        animScreen.SetActive(false);
        PostMazeTxt.SetActive(false);
    }


    public void toMaze()
    {
        StartCoroutine(waitAndAnimToMaze());
    }


    void LoadPlayerData()
    {

    }


}
