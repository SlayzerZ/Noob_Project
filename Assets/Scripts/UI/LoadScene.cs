using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string SceneName;
    private Animator FadeSys;
    public bool Menu = false;

    private void Awake()
    {
        FadeSys = GameObject.FindGameObjectWithTag("FadeSys").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    public IEnumerator LoadNextScene()
    {
        if (!Menu)
        {
            if(LevelManager.Instance.levelR > SaveData.Instance.levelLoadData(PlayerController.Instance.Name))
            {
                SaveData.Instance.levelSaveData(LevelManager.Instance.levelR);
            }
            SaveData.Instance.saveData();
        }
        FadeSys.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneName);
    }

    public void toMainMenu()
    {
        StartCoroutine(LoadNextScene());
    }
}
