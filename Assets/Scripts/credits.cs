using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loadMainMenu();
        }
    }
}
