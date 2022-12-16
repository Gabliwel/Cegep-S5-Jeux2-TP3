using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] float health;
    float MAX_HEALTH = 0;
    void Start()
    {
        MAX_HEALTH = health;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public void LoseHp()
    {
        health -= 1;
        float healthOpacity = health / MAX_HEALTH;
        if (healthOpacity < 0.2f)
        {
            healthOpacity = 0.2f;
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, healthOpacity);
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
