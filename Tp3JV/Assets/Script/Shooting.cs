using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private GameObject player;
    [SerializeField] GameObject projectilePrefab;
    private static int projectileTotal = 1;
    private GameObject[] projectile = new GameObject[projectileTotal];

    private float SHOOTING_DELAY = 3;
    private float currentWait = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < projectileTotal; i++)
        {
            projectile[i] = Instantiate(projectilePrefab);
            projectile[i].SetActive(false);
        }
    }

    void Update()
    {
        if (player.activeSelf)
        {
            currentWait += Time.deltaTime;
            if (currentWait >= SHOOTING_DELAY)
            {
                currentWait = 0;
                for (int i = 0; i < projectileTotal; i++)
                {
                    if (!projectile[i].activeSelf)
                    {
                        projectile[i].transform.position = transform.position;
                        projectile[i].GetComponent<Projectile>().SetDirection(player.transform.position);
                    }
                }
            }
        }
    }
}
