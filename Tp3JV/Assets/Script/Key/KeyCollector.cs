using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    [SerializeField] private int neededKey;
    private int currentKey = 0;
    [SerializeField]private AudioSource source;
    private AudioClip collectedSound;

    private void Start()
    {
        collectedSound = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().KeyCollected;
    }

    public void AddKey()
    {
        currentKey++;
        source.PlayOneShot(collectedSound);
    }

    public bool HasEnoughKeys()
    {
        return currentKey >= neededKey;
    }
}
