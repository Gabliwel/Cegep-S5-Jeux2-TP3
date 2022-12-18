using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCMovement : MonoBehaviour
{
    protected bool willChase = false;
    void Start()
    {

    }

    public abstract void Movement();

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }


        Movement();
    }

    public void SwitchChase()
    {
        willChase = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && gameObject.tag != "Ally")
        {
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().StopGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().StopGame();
        }
    }
}
