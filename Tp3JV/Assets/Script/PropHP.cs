using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHP : MonoBehaviour
{
    [SerializeField] float health;
    private float MAX_HEALTH = 0;
    private SpriteRenderer sprite;


    [SerializeField] private bool isWood;
    private SoundMaker soundMaker;

    private ParticleSystem system;

    void Start()
    {
        system = gameObject.GetComponentInChildren<ParticleSystem>();
        MAX_HEALTH = health;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        soundMaker = GameObject.FindGameObjectWithTag("SoundMaker").GetComponent<SoundMaker>();
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
        if(healthOpacity <= 0.5f && system != null && !system.isPlaying) system.Play();
        if (health <= 0)
        {
            StartCoroutine(LastSeconds());
        }
    }

    private IEnumerator LastSeconds()
    {
        sprite.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(0.6f);
        if(isWood)
        {
            soundMaker.BrokenWoodProp(gameObject.transform.position);
        }
        else
        {
            soundMaker.BrokenRockProp(gameObject.transform.position);
        }
        gameObject.SetActive(false);
    }
}
