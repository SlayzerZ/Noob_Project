using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelbuttons1;
    private string chara1 = "Sonic";

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (!File.Exists(SaveData.Instance.savePath()))
        {
            SaveData.Instance.CreateDefaultSaveFile(chara1);
        }
        int levelReached = SaveData.Instance.levelLoadData(chara1);
        for (int i = 0; i < levelbuttons1.Length; i++)
        {
            if (i > levelReached)
            {
                levelbuttons1[i].interactable = false;
            }
        }
    }
    public void LoadLevelPassed(string level)
    {
        SceneManager.LoadScene(level);
    }
}
