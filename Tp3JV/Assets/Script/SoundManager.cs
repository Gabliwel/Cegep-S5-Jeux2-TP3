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

    [SerializeField] private AudioClip endZone;

    public static SoundManager Instance { get { return instance; } }
    public AudioClip KeyCollected { get { return keyCollected; } }
    public AudioClip BlockedDoor { get { return blockedDoor; } }
    public AudioClip OpenDoor { get { return openDoor; } }
    public AudioClip Pause { get { return pause; } }
    public AudioClip Unpause { get { return unpause; } }

    public AudioClip EndZone { get { return endZone; } }

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
