using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    [SerializeField] private int neededKey;
    private int currentKey = 0;

    public void AddKey()
    {
        currentKey++;
    }

    public bool HasEnoughKeys()
    {
        return currentKey >= neededKey;
    }
}
