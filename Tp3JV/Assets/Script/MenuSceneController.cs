using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class MenuSceneController : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(TodoOnClick);
    }

    private void TodoOnClick()
    {
        gameManager.StartGame();
    }
}
