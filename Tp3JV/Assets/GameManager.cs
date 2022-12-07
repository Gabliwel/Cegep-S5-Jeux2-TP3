using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    private GameObject playerInstance;
    bool gameOver = false;

    public static GameManager instance;

    private int actualLevel = 0;

    bool scenesAreInTransition = false;

    private int life = 3;

    private float currentDelay = 0;
    private float MAX_DELAY = 1;
    private bool deathWait = false;

    void Awake()
    {
        if (instance == null)
        {
            playerInstance = player;
            instance = this;
        }

        else if (instance != this)
        {
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
        
    }

    public void StopGame()
    {
        life -= 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<LookingAt>().Disable();
    }

    public void RestartLevel(float delay)
    {
        if (scenesAreInTransition) return;

        scenesAreInTransition = true;

        deathWait = false;

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


        if (level == 0)
        {
            SceneManager.LoadScene("SceneGab");
        }
        else if (level == 2)
            SceneManager.LoadScene("Scene2");
        else if (level == 3)
            SceneManager.LoadScene("Scene3");
        else if (level == 1)
        {
            SceneManager.LoadScene("SceneCharles");
        }


        scenesAreInTransition = false;
    }
}
