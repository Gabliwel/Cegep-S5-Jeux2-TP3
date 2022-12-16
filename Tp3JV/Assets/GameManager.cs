using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    private GameObject playerInstance;
    bool gameOver = false;

    public static GameManager instance;

    private int actualLevel = 0;

    bool scenesAreInTransition = false;

    private int life = 3;

    private float timer = 0;
    [SerializeField] Text displayedTimer;
    [SerializeField] Text displayedLives;
    private bool textsNotLinked = true;
    private bool checkEnnemies = false;

    private int totalEnnemies = 0;

    void Awake()
    {
        if (instance == null)
        {
            playerInstance = player;
            checkEnnemies = true;
            instance = this;
        }

        else if (instance != this)
        {
            actualLevel = instance.actualLevel;
            timer = instance.timer;
            life = instance.life;
            playerInstance = instance.playerInstance;
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }


    void Update()
    {
        linkTexts();
        lookingForEnnemies();
        if (actualLevel != 0 && actualLevel != 1)
        {
            if (displayedTimer != null && playerInstance.activeSelf)
            {
                timer += Time.deltaTime;
                displayedTimer.text = (Mathf.Round(timer * 100f) / 100).ToString();
            }
        }
    }

    public void StartGame()
    {
        actualLevel = 2;
        StartCoroutine(RestartLevelDelay(3, actualLevel));
    }

    public void StopGame()
    {
        life -= 1;
        Debug.Log(life);
        GameObject.FindGameObjectWithTag("Player").GetComponent<LookingAt>().Disable();
        RestartLevel(2f);
    }

    public void DeadEnnemie()
    {
        totalEnnemies -= 1;
        Debug.Log(totalEnnemies);
        if (totalEnnemies <= 0)
        {
            actualLevel += 1;
            StartCoroutine(RestartLevelDelay(3, actualLevel));
        }
    }


    private void linkTexts()
    {
        if (textsNotLinked)
        {
            textsNotLinked = false;
            if (actualLevel == 0) return;

            displayedTimer = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
            displayedTimer.text = (Mathf.Round(timer * 100f) / 100).ToString();

            displayedLives = GameObject.FindGameObjectWithTag("Life").GetComponent<Text>();
            displayedLives.text = life.ToString();

            playerInstance = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void lookingForEnnemies()
    {
        if (checkEnnemies)
        {
            checkEnnemies = false;
            totalEnnemies = GameObject.FindGameObjectsWithTag("Ennemie").Length;
            Debug.Log(totalEnnemies);
        }
    }

    public void RestartLevel(float delay)
    {
        if (scenesAreInTransition) return;

        scenesAreInTransition = true;


        if(life <= 0)
        {
            actualLevel = 1;
        }

        StartCoroutine(RestartLevelDelay(delay, actualLevel));
    }

    private IEnumerator RestartLevelDelay(float delay, int level)
    {
        Debug.Log("Got to scene");
        yield return new WaitForSeconds(delay);
        textsNotLinked = true;
        checkEnnemies = true;



        if (level == 0)
        {
            SceneManager.LoadScene("Starting_Menu");
        }
        else if (level == 2)
        {
            SceneManager.LoadScene("SceneGab");
        }
        else if (level == 3)
            SceneManager.LoadScene("SceneGab4_2");
        else if (level == 1)
        {
            SceneManager.LoadScene("End");
        }


        scenesAreInTransition = false;
    }

    /*******************************/

    public GameObject getPlayer()
    {
        return playerInstance;
    }
}
