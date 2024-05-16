using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int coinsPickup;
    public Vector3 respawnPoint;
    public int levelR;

    public static LevelManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il ya déjà une instance de LevelManager.");
            return;
        }
        Instance = this;

        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
