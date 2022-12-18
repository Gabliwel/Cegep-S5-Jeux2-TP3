using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    [SerializeField] private static int arrayLenght = 15;
    [SerializeField] private GameObject individualSoundMaker;

    private GameObject[] soundMakerArray = new GameObject[arrayLenght];
    private SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        for (int i = 0; i < arrayLenght; i++)
        {
            soundMakerArray[i] = Instantiate(individualSoundMaker);
            soundMakerArray[i].SetActive(false);
        }
    }

    public void DeadEnemy(Vector2 position)
    {
        RequestSound(position, soundManager.DeadEnemy);
    }

    public void StartSound(Vector2 position)
    {
        RequestSound(position, soundManager.StartSound);
    }

    public void DeadPlayer(Vector2 position)
    {
        RequestSound(position, soundManager.DeadPlayer);
    }

    public void DeadAlly(Vector2 position)
    {
        RequestSound(position, soundManager.DeadAlly);
    }

    public void BrokenWoodProp(Vector2 position)
    {
        RequestSound(position, soundManager.BrokenWoodProp);
    }

    public void BrokenRockProp(Vector2 position)
    {
        RequestSound(position, soundManager.BrokenRockProp);
    }

    private void RequestSound(Vector2 position, AudioClip audioClip)
    {
        foreach(GameObject individual in soundMakerArray)
        {
            if (!individual.activeSelf)
            {
                individual.SetActive(true);
                individual.GetComponent<IndividualSoundMaker>().PlayAtPoint(audioClip, position);

                return;
            }
        }
    }
}
