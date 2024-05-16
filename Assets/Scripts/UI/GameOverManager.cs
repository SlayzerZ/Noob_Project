using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUi;
    // Start is called before the first frame update
    public static GameOverManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il ya déjà une instance de GameOver.");
            return;
        }
        Instance = this;
    }

    public void onPlayerDeath()
    {
        gameOverUi.SetActive(true);
        Inventory.Instance.RemoveCoins(LevelManager.Instance.coinsPickup);
        SaveData.Instance.resetSoftData();
    }

    public void retryButton()
    {
       // PlayerHealth.Instance.Respawn();
        gameOverUi.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
