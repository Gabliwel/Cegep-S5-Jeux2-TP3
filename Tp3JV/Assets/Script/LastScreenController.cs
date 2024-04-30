using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastScreenController : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        StartCoroutine(GoToMenu(5));
    }

    private IEnumerator GoToMenu(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameManager.GoToMenu();
    }
}
