using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesHealth : MonoBehaviour
{
    [SerializeField] int health;
    Animator animator;
    private bool gotHit = false;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
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
        animator.SetBool("isHit", true);
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        if (!gotHit)
        {
            gotHit = true;
            if (GetComponent<NPCMovement>() != null)
            {
                GetComponent<NPCMovement>().SwitchChase();
            }
        }
    }

    public void StopHitAnimation()
    {
        animator.SetBool("isHit", false);
    }
}
