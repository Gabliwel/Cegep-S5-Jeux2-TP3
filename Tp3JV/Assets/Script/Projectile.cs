using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int speed = 1;
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

        transform.rotation = Quaternion.LookRotation(Vector3.forward, playerSpot);
        transform.position += direction * Time.deltaTime;
    }

    public void SetDirection(Vector3 playerLocation)
    {
        playerSpot = playerLocation;
        direction = (playerLocation - transform.position).normalized * speed;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Ennemie")
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ennemie")
        {
            gameObject.SetActive(false);
        }
    }
}
