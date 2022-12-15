using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCmovement : MonoBehaviour
{
    [SerializeField] Vector3[] positionPoint;
    [SerializeField] float speed;
    private Vector3 destination;
    private int currentPosition = 0;
    private bool firstDest = false;
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
            Debug.Log(positionPoint.Length);
            destination = positionPoint[0];

        }

        Vector2 towards = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);

        GetComponent<Rigidbody2D>().MovePosition(towards);

        if(transform.position == destination)
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
            }
            if(currentPosition + 1 < positionPoint.Length)
            {
                destination = positionPoint[currentPosition + 1];
            }
        }
    }

    private Vector3[] ReverseDestination()
    {
        Vector3[] reversed = new Vector3[positionPoint.Length - 1];
        for(int i = positionPoint.Length - 1; i > 0; i--)
        {

        }
        return reversed;
    }
}
