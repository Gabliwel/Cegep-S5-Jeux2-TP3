using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPause = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().getPlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeState();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPause = true;
        player.GetComponent<LookingAt>().changePause();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPause = false;
        player.GetComponent<LookingAt>().changePause();
    }

    public void ChangeState()
    {
        Debug.Log("Paused");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().getPlayer();
        }
        if (isPause)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
