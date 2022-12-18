using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDamage : MonoBehaviour
{
    private SoundMaker soundMaker;

    void Start()
    {
        soundMaker = GameObject.FindGameObjectWithTag("SoundMaker").GetComponent<SoundMaker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Laser" && gameObject.tag == "Ally")
        {
            soundMaker.DeadAlly(gameObject.transform.position);
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().StopGame(false);
            gameObject.SetActive(false);
        }
    }

}
