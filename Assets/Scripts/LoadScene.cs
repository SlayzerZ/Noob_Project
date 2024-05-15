using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string SceneName;
    public Animator FadeSys;
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
        FadeSys.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        if (Menu)
        {
            DontDestroyOnLoadS.Instance.RemoveFromDD();
        }
        SceneManager.LoadScene(SceneName);
    }

    public void toMainMenu()
    {
        StartCoroutine(LoadNextScene());
    }
}
