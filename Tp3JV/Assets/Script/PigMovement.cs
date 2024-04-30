using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMovement : NPCMovement
{
    [SerializeField] Vector3[] positionPoint;
    [SerializeField] float speed;
    private bool canChase = true;
    private bool charge = true;
    private Vector3 destination;
    Animator animator;
    private int currentPosition = 0;
    private bool firstDest = false;
    private bool canMove = true;


    private float MAX_CHARGE_WAIT = 3;
    private float currentChargeWait = 0;

    private float MAX_TIME_CHARGING = 2;
    private float currentTimeCharging = 0;
    private bool chargeReady = false;

    private GameObject player;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        if (canChase || charge)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public override void Movement()
    {
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

        animator.SetBool("isWalking", true);
        if (willChase)
        {
            Chase();
        }

        if (charge && !chargeReady)
        {
            ChargeCountdown();
        }

        if (chargeReady && willChase)
        {
            Charging();
        }
        else
        {
            NormalWalking();
        }

        if (transform.position == destination && canMove)
        {
            setPosition();
        }
    }

    public void NormalWalking()
    {

        Vector2 towards = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);

        if (destination.x - transform.position.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        GetComponent<Rigidbody2D>().MovePosition(towards);
    }

    public void Chase()
    {

        if (player.activeSelf)
        {
            destination = player.transform.position;
        }
    }

    public void ChargeCountdown()
    {

        currentChargeWait += Time.deltaTime;
        if (currentChargeWait >= MAX_CHARGE_WAIT)
        {
            currentChargeWait = 0;
            chargeReady = true;
        }
    }

    public void Charging()
    {

        Vector2 towards = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed * 2);

        if (player.transform.position.x - transform.position.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        GetComponent<Rigidbody2D>().MovePosition(towards);
    }

    public void ChargingTime()
    {
        currentTimeCharging += Time.deltaTime;
        if (currentTimeCharging >= MAX_TIME_CHARGING)
        {
            chargeReady = false;
            currentTimeCharging = 0;

            if (positionPoint.Length == 0)
            {
                destination = transform.position;
            }
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
            if (currentPosition + 1 < positionPoint.Length)
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


}
