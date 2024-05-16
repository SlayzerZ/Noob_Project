using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
        
    }
    private void Start()
    {
        LevelManager.Instance.respawnPoint = transform.position;
    }
}
