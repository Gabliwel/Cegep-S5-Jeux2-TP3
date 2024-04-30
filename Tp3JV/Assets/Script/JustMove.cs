using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustMove : NPCMovement
{
    [SerializeField] Vector3[] positionPoint;
    [SerializeField] float speed;
    [SerializeField] bool doesntHaveAnime;
    private Vector3 destination;
    Animator animator;
    private int currentPosition = 0;
    private bool firstDest = false;
    private bool canMove = true;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
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

        if (!doesntHaveAnime)
        {
            animator.SetBool("isWalking", true);
        }
        NormalWalking();

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
