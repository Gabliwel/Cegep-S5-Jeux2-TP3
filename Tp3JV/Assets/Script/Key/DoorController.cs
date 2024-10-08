using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject startObject;
    [SerializeField] private GameObject endObject;
    [SerializeField] private TMP_Text doorUi;
    private SoundManager soundManager;
    private AudioSource audioSource;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        audioSource = GetComponent<AudioSource>();
        startObject.SetActive(true);
        endObject.SetActive(false);
        doorUi.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!endObject.activeSelf)
            {
                if(collision.gameObject.GetComponent<KeyCollector>().HasEnoughKeys())
                {
                    startObject.SetActive(false);
                    endObject.SetActive(true);
                    audioSource.PlayOneShot(soundManager.OpenDoor);
                }
                else
                {
                    audioSource.PlayOneShot(soundManager.BlockedDoor);
                    doorUi.text = "Il vous manque des cl�s";
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!endObject.activeSelf)
        {
            doorUi.text = "";
        }
    }
}
