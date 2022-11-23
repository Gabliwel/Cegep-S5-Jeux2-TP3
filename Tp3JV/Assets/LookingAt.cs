using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAt : MonoBehaviour
{
    private Vector3 _target;
    public Camera Camera;
    [SerializeField] GameObject ray;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _target = Camera.ScreenToWorldPoint(Input.mousePosition);
        _target.z = 0;
        Vector3 targeting = new Vector3(_target.x, _target.y, 0);

        //ray.transform.localScale = 

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targeting);
        if(hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.tag);
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            float distanceX = Mathf.Abs(hit.point.x - transform.position.x);
            if (distanceX > distance)
            {
                distance = distanceX;
            }
            Vector3 spot = new Vector3(hit.point.x, hit.point.y, 0);
            Vector3 position = (transform.position + spot) / 2;

            ray.transform.rotation = Quaternion.LookRotation(Vector3.forward, position - transform.position) * Quaternion.Euler(0, 0, 90);
            ray.transform.position = position;
            ray.transform.localScale = new Vector3(distance, 1, 1);
            Debug.Log(distance);
        }
        //gameObject.transform.position = _target;
    }
}
