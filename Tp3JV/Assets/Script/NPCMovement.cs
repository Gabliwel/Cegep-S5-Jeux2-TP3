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
    Animator animator;
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
        animator = gameObject.GetComponent<Animator>();
        if(canChase || charge)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        animator.SetBool("isWalking", true);
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
            animator.SetBool("isWalking", true);
            if (player.activeSelf)
            {
                destination = player.transform.position;
            }
        }

        if (charge && !chargeReady)
        {
            animator.SetBool("isWalking", true);
            currentChargeWait += Time.deltaTime;
            if(currentChargeWait >= MAX_CHARGE_WAIT)
            {
                currentChargeWait = 0;
                chargeReady = true;
            }
        }

        if (chargeReady && willChase)
        {
            animator.SetBool("isWalking", true);
            Vector2 towards = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed * 2);
            if(player.transform.position.x - transform.position.x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            GetComponent<Rigidbody2D>().MovePosition(towards);
            animator.SetBool("isWalking", true);
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
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            animator.SetBool("isWalking", true);
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
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                ReverseDestination();
                destination = positionPoint[1];
            }
            if(currentPosition + 1 < positionPoint.Length)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                destination = positionPoint[currentPosition + 1];
                currentPosition++;
            }
        }
    }

    private void ReverseDestination()
    {
        Vector3[] reversed = new Vector3[positionPoint.Length];
        int counter = 0;
        for (int i = positionPoint.Length; i > 0; i--)
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
