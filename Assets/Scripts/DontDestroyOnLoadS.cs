using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadS : MonoBehaviour
{
    public GameObject[] objects;

    public static DontDestroyOnLoadS Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il ya déjà une instance de DontDestroy.");
            return;
        }
        Instance = this;
        foreach (var obj in objects)
        {
            DontDestroyOnLoad(obj);
        }
    }

    public void RemoveFromDD()
    {
        foreach (var obj in objects)
        {
            SceneManager.MoveGameObjectToScene(obj,SceneManager.GetActiveScene());
        }
    }
}
