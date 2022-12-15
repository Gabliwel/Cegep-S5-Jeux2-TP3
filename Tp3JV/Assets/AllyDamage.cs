using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Laser")
        {
            gameObject.SetActive(false);
            if (gameObject.tag == "Ally")
            {
                GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().StopGame();
            }
            if(gameObject.tag == "Ennemie")
            {
                GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().DeadEnnemie();
            }
        }
    }

}
