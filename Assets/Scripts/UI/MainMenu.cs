using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad;
    public GameObject settingsWindows;

    public void StartGame()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Settings()
    {
        settingsWindows.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void CloseSettings()
    {
        settingsWindows.SetActive(false);
    }
}
