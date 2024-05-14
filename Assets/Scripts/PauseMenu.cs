using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject SettingsUI;
    private float JF;
    private bool isSettings = false;
    public AudioClip pauseSound;
    public AudioClip exitSound;
    public AudioClip settingsSound;

    private void Start()
    {
        PlayerController.Instance.movement.Movement.Pause.performed += Paused;
        JF = PlayerController.Instance.jumpForce;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        PlayerController.Instance.jumpForce = 0;
    }

    public void Resume()
    {
        AudioManager.Instance.playAtPoint(exitSound, transform.position);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        PlayerController.Instance.jumpForce = JF;
    }

    private void Paused(InputAction.CallbackContext context)
    {
        if (gamePaused)
        {
            Resume();
        }
        else
        {
            AudioManager.Instance.playAtPoint(pauseSound, transform.position);
            Pause();
        }
    }

    public void MainMenu()
    {
        DontDestroyOnLoadS.Instance.RemoveFromDD();
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsMenu()
    {
        if (!isSettings)
        {
            isSettings = true;
        } else
        {
            isSettings = false;
        }
        AudioManager.Instance.playAtPoint(settingsSound,transform.position);
        SettingsUI.SetActive(isSettings);
    }
}
