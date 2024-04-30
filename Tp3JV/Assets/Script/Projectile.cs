using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int speed = 3;
    private Vector3 direction;
    private float MAX_TIME_ALIVE = 15;
    private float currentTime = 0;
    private Vector3 playerSpot;
    void Start()
    {
        
    }

    
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= MAX_TIME_ALIVE)
        {
            currentTime = 0;
            gameObject.SetActive(false);
        }

        float angle = Mathf.Atan2(playerSpot.y, playerSpot.x);

        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);

        transform.position += direction * Time.deltaTime;
    }

    public void SetDirection(Vector3 playerLocation)
    {
        playerSpot = playerLocation - transform.position;
        direction = (playerLocation - transform.position).normalized * speed;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ennemie") return;

        if (collision.gameObject.tag != "Player")
        {
            gameObject.SetActive(false);
        } 
        else
        {
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().StopGame(true);
            gameObject.SetActive(false);
        }
    }
}
