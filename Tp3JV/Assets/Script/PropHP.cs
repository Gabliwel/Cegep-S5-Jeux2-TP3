using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHP : MonoBehaviour
{
    [SerializeField] float health;
    private float MAX_HEALTH = 0;
    private SpriteRenderer sprite;

    void Start()
    {
        MAX_HEALTH = health;
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void LoseHp()
    {
        if (health <= 0)
            return;

        health -= 1;
        float healthOpacity = health / MAX_HEALTH;
        if (healthOpacity < 0.2f)
        {
            healthOpacity = 0.2f;
        }
        sprite.color = new Color(healthOpacity, healthOpacity, healthOpacity, 1);
        if (health <= 0)
        {
            StartCoroutine(LastSeconds());
        }
    }

    private IEnumerator LastSeconds()
    {
        sprite.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
