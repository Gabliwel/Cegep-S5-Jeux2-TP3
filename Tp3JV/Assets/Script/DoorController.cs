using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject startObject;
    [SerializeField] private GameObject endObject;
    [SerializeField] private TMP_Text doorUi;

    // Start is called before the first frame update
    void Start()
    {
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
                }
                else
                {
                    doorUi.text = "Il vous manque des clés";
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
