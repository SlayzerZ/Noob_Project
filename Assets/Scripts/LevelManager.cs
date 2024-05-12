using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool playerPresentbyDefault = false;
    public int coinsPickup;

    public static LevelManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il ya déjà une instance de LevelManager.");
            return;
        }
        Instance = this;
    }
}
