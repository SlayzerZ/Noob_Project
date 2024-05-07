using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] objects;
    void Awake()
    {
        foreach (var obj in objects)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
