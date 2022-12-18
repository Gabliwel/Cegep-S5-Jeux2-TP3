using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    private GameObject playerInstance;

    public static GameManager instance;

    private int actualLevel = 0;
    bool scenesAreInTransition = false;

    private int life = 3;
    private float timer = 0;

    private Text displayedTimer;
    private Text displayedLives;
    private Text pauseText;
    private RawImage pauseBack;
    private Text status;
    private bool textsNotLinked = true;
    private SoundMaker soundMaker;

    void Awake()
    {
        if (instance == null)
        {
            playerInstance = player;
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
        actualLevel = SceneManager.GetActiveScene().buildIndex;
    }


    void Update()
    {
        linkTexts();
        if (actualLevel == 0 || actualLevel == 8) return;
        if (actualLevel == 7)
        {
            displayedTimer.text = (Mathf.Round(timer * 100f) / 100).ToString() + " secondes";
        }
        else
        {
            if (displayedTimer != null && playerInstance.activeSelf)
            {
                timer += Time.deltaTime;
                displayedTimer.text = (Mathf.Round(timer * 100f) / 100).ToString();
            }
        }

    }

    public void GoToMenu()
    {
        actualLevel = 0;
        StartCoroutine(RestartLevelDelay(0, actualLevel));
    }

    public void StartGame()
    {
        actualLevel = 1;
        life = 3;
        timer = 0;
        StartCoroutine(RestartLevelDelay(0, actualLevel));
    }

    public void StopGame(bool killed)
    {
        life -= 1;
        playerInstance.GetComponent<LookingAt>().Disable();
        if(killed)
        {
            soundMaker.DeadPlayer(playerInstance.transform.position);
            status.text = "Vous êtes mort par un ennemie!";
        }
        else
        {
            status.text = "Ne tuez pas vos alliers!";
        }
        RestartLevel(2f);
    }

    private void linkTexts()
    {
        if (textsNotLinked)
        {
            textsNotLinked = false;
            if (actualLevel == 0 || actualLevel == 8) return;

            displayedTimer = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
            displayedTimer.text = (Mathf.Round(timer * 100f) / 100).ToString();

            if (actualLevel != 7)
            {
                displayedLives = GameObject.FindGameObjectWithTag("Life").GetComponent<Text>();
                displayedLives.text = life.ToString() + " vies";

                playerInstance = GameObject.FindGameObjectWithTag("Player");

                pauseText = GameObject.FindGameObjectWithTag("PauseUI").GetComponent<Text>();
                pauseText.enabled = false;

                pauseBack = GameObject.FindGameObjectWithTag("PauseUIBack").GetComponent<RawImage>();
                pauseBack.enabled = false;

                status = GameObject.FindGameObjectWithTag("StatusUI").GetComponent<Text>();
                status.text = "";

                soundMaker = GameObject.FindGameObjectWithTag("SoundMaker").GetComponent<SoundMaker>();
                soundMaker.StartSound(playerInstance.transform.position);
            }
        }
    }

    public void RestartLevel(float delay)
    {
        if (scenesAreInTransition) return;
        scenesAreInTransition = true;


        if(life <= 0)
        {
            actualLevel = 8;
        }

        StartCoroutine(RestartLevelDelay(delay, actualLevel));
    }

    internal void StartNextLevel()
    {
        if (scenesAreInTransition) return;
        scenesAreInTransition = true;

        actualLevel++;

        StartCoroutine(RestartLevelDelay(1f, actualLevel));
    }

    private IEnumerator RestartLevelDelay(float delay, int level)
    {
        yield return new WaitForSeconds(delay);
        textsNotLinked = true;

        if (level == 0)
            SceneManager.LoadScene("Starting_Menu");
        else if (level == 1)
            SceneManager.LoadScene("SceneGab2");
        else if (level == 2)
            SceneManager.LoadScene("SceneGab3");
        else if (level == 3)
            SceneManager.LoadScene("SceneGab4_2");
        else if (level == 4)
            SceneManager.LoadScene("SceneGab5");
        else if (level == 5)
            SceneManager.LoadScene("SceneGab6");
        else if (level == 6)
            SceneManager.LoadScene("SceneGab7");
        else if (level == 7)
            SceneManager.LoadScene("Victoire");
        else if (level == 8)
            SceneManager.LoadScene("End");

        scenesAreInTransition = false;
    }

    public bool CanPause()
    {
        return actualLevel != 7 && actualLevel != 8 && actualLevel != 0;
    }

    public void PauseUI(bool onPause)
    {
        pauseBack.enabled = onPause;
        pauseText.enabled = onPause;
    }
}
