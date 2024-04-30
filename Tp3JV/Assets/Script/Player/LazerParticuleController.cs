using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerParticuleController : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
    }
}
