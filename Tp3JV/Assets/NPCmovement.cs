using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCmovement : MonoBehaviour
{
    [SerializeField] Vector3[] positionPoint;
    [SerializeField] float speed;
    [SerializeField] bool canChase;
    private Vector3 destination;
    private int currentPosition = 0;
    private bool firstDest = false;
    private bool canMove = true;
    private bool willChase = false;

    private GameObject player;
    void Start()
    {
        
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (!firstDest)
        {
            firstDest = true;
            if (positionPoint.Length > 0)
            {
                destination = positionPoint[0];
            }
            else
            {
                canMove = false;
                destination = transform.position;
            }
        }

        if (willChase)
        {
            if (player.activeSelf)
            {
                destination = player.transform.position;
            }
        }

        Vector2 towards = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);

        GetComponent<Rigidbody2D>().MovePosition(towards);

        if(transform.position == destination && canMove)
        {
            setPosition();
        }
    }

    private void setPosition()
    {
        if (positionPoint.Length > 0)
        {
            if (currentPosition == positionPoint.Length - 1)
            {
                ReverseDestination();
                destination = positionPoint[1];
            }
            if(currentPosition + 1 < positionPoint.Length)
            {
                destination = positionPoint[currentPosition + 1];
                currentPosition++;
            }
        }
    }

    private void ReverseDestination()
    {
        Vector3[] reversed = new Vector3[positionPoint.Length];
        int counter = 0;
        for(int i = positionPoint.Length; i > 0; i--)
        {
            reversed[counter] = positionPoint[i - 1];
            counter++;
        }
        currentPosition = 0;
        positionPoint = reversed;
    }

    public void SwitchChase()
    {
        if (canChase)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            willChase = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().StopGame();
        }
    }
}
