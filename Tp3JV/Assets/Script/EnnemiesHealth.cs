using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesHealth : MonoBehaviour
{
    [SerializeField] int health;
    private bool gotHit = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    public void LoseHp()
    {
        health -= 1;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        if (!gotHit)
        {
            if (GetComponent<NPCMovement>() != null)
            {
                GetComponent<NPCMovement>().SwitchChase();
            }
        }
    }
}
