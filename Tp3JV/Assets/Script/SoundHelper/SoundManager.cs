using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    [SerializeField] private AudioClip keyCollected;
    [SerializeField] private AudioClip blockedDoor;
    [SerializeField] private AudioClip openDoor;

    [SerializeField] private AudioClip pause;
    [SerializeField] private AudioClip unpause;

    [SerializeField] private AudioClip startSound;
    [SerializeField] private AudioClip endZone;

    [SerializeField] private AudioClip deadPlayer;
    [SerializeField] private AudioClip deadEnemy;
    [SerializeField] private AudioClip deadAlly;

    [SerializeField] private AudioClip brokenWoodProp;
    [SerializeField] private AudioClip brokenRockProp;

    public static SoundManager Instance { get { return instance; } }
    public AudioClip KeyCollected { get { return keyCollected; } }
    public AudioClip BlockedDoor { get { return blockedDoor; } }
    public AudioClip OpenDoor { get { return openDoor; } }
    public AudioClip Pause { get { return pause; } }
    public AudioClip Unpause { get { return unpause; } }
    public AudioClip StartSound { get { return startSound; } }
    public AudioClip EndZone { get { return endZone; } }
    public AudioClip DeadPlayer { get { return deadPlayer; } }
    public AudioClip DeadEnemy { get { return deadEnemy; } }
    public AudioClip DeadAlly { get { return deadAlly; } }
    public AudioClip BrokenWoodProp { get { return brokenWoodProp; } }
    public AudioClip BrokenRockProp { get { return brokenRockProp; } }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }
}
