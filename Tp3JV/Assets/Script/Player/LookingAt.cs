using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAt : MonoBehaviour
{
    private Vector3 _target;
    [SerializeField] GameObject ray;
    [SerializeField] Vector3 hitting;
    float ScreenMiddleX = 0;
    float ScreenMiddleY = 0;
    float offSet = 0;
    bool gamePaused = false;

    private GameObject previousHit;

    void Start()
    {
        Physics2D.queriesHitTriggers = false;
        Physics2D.queriesStartInColliders = false;

        Vector3 middleOfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        ScreenMiddleX = middleOfScreen.x;
        ScreenMiddleY = middleOfScreen.y;
        offSet = (ScreenMiddleX - ScreenMiddleY) / 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Button")
        {
            Debug.Log("Button");
        }
        if (collision.gameObject.tag == "Projectile" && !collision.isTrigger)
        {
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().StopGame();
        }
    }


    public void Disable()
    {
        ray.SetActive(false);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (gamePaused)
        {
            return;
        }

        _target = Input.mousePosition;
        _target.y = (_target.y - ScreenMiddleY) / ScreenMiddleY;

        if (_target.x < offSet)
        {
            _target.x = (_target.x - ScreenMiddleX) / ScreenMiddleX - 1;
        }
        else
        {
            _target.x = (_target.x - ScreenMiddleX) / ScreenMiddleX * (ScreenMiddleX / ScreenMiddleY);
        }

        Vector3 middle = gameObject.transform.position;
        middle.y += 0.25f;

        RaycastHit2D hit = Physics2D.Raycast(middle, _target);
        if (hit.collider != null)
        {
            float distanceY = Mathf.Pow(Mathf.Abs(hit.point.y - middle.y), 2);
            float distanceX = Mathf.Pow(Mathf.Abs(hit.point.x - middle.x), 2);
            float distance = Mathf.Sqrt(distanceX + distanceY);

            Vector3 spot = new Vector3(hit.point.x, hit.point.y, 0);
            Vector3 position = (middle + spot) / 2;

            ray.transform.rotation = Quaternion.LookRotation(Vector3.forward, position - middle) * Quaternion.Euler(0, 0, 90);
            ray.transform.position = position;
            ray.transform.localScale = new Vector3(distance, 0.1f, 1);
            ray.GetComponent<Collider2D>().transform.localScale = new Vector3(distance, 0.1f, 1);


            if (hit.collider.gameObject.tag == "Prop" && hit.collider.gameObject.activeSelf)
            {
                hit.collider.gameObject.GetComponent<PropHP>().LoseHp();
            }
            if (hit.collider.gameObject.tag == "Ennemie" && hit.collider.gameObject.activeSelf)
            {
                hit.collider.gameObject.GetComponent<EnnemiesHealth>().LoseHp();
            }
            if (previousHit != null)
            {
                if (previousHit.tag == "Ennemie" && hit.collider.gameObject != previousHit)
                {
                    previousHit.GetComponent<EnnemiesHealth>().StopHitAnimation();
                }
            }
            previousHit = hit.collider.gameObject;
        }
    }
        public void ChangePauseState(bool state)
        {
            gamePaused = state;
        }
}
