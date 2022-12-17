using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] Vector3[] positionPoint;
    [SerializeField] float speed;
    [SerializeField] bool canChase;
    [SerializeField] bool charge;
    private Vector3 destination;
    private int currentPosition = 0;
    private bool firstDest = false;
    private bool canMove = true;
    private bool willChase = false;

    private float MAX_CHARGE_WAIT = 3;
    private float currentChargeWait = 0;

    private float MAX_TIME_CHARGING = 2;
    private float currentTimeCharging = 0;
    private bool chargeReady = false;

    private GameObject player;
    void Start()
    {

        if(canChase || charge)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
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

        if (charge && !chargeReady)
        {
            currentChargeWait += Time.deltaTime;
            if(currentChargeWait >= MAX_CHARGE_WAIT)
            {
                currentChargeWait = 0;
                chargeReady = true;
            }
        }

        if (chargeReady && willChase)
        {
            Vector2 towards = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed * 2);

            GetComponent<Rigidbody2D>().MovePosition(towards);

            currentTimeCharging += Time.deltaTime;
            if(currentTimeCharging >= MAX_TIME_CHARGING)
            {
                chargeReady = false;
                currentTimeCharging = 0;

                if(positionPoint.Length == 0)
                {
                    destination = transform.position;
                }
            }
        }
        else
        {
            Vector2 towards = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);

            GetComponent<Rigidbody2D>().MovePosition(towards);
        }


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
            willChase = true;
        }
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
