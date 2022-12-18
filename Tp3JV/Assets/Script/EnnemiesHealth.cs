using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesHealth : MonoBehaviour
{
    [SerializeField] int health;
    Animator animator;
    private bool gotHit = false;
    private SoundMaker soundMaker;

    void Start()
    {
        soundMaker = GameObject.FindGameObjectWithTag("SoundMaker").GetComponent<SoundMaker>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void LoseHp()
    {
        health -= 1;
        animator.SetBool("isHit", true);
        if (health <= 0)
        {
            soundMaker.DeadEnemy(gameObject.transform.position);
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
