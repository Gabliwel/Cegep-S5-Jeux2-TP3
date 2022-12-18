using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullMovement : NPCMovement
{
    [SerializeField] float speed;
    private bool canChase = true;
    private bool charge = true;
    private Vector3 destination;
    Animator animator;

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
        if (willChase)
        {
            Chase();
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
            NormalWalking();
        }
    }
}
