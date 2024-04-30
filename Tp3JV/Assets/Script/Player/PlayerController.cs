using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement movement;
    private LookingAt lazer;

    void Start()
    {
        movement = GetComponent<CharacterMovement>();
        lazer = GetComponent<LookingAt>();
    }

    public void ChangePauseState(bool isPaused)
    {
        lazer.ChangePauseState(isPaused);
        movement.ChangePauseState(isPaused);
    }
}
