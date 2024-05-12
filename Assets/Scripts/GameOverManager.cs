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
        if (LevelManager.Instance.playerPresentbyDefault)
        {
            DontDestroyOnLoadS.Instance.RemoveFromDD();
        }
        gameOverUi.SetActive(true);
    }

    public void retryButton()
    {
        Inventory.Instance.RemoveCoins(LevelManager.Instance.coinsPickup);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerHealth.Instance.Respawn();
        gameOverUi.SetActive(false);
    }

    public void mainMenuButton()
    {

    }

    public void quitButton()
    {
        Application.Quit();
    }
}
