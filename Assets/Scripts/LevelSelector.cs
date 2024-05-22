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
    public Button[] levelbuttons2;
    private string chara1 = "Sonic";
    private string chara2 = "Amy";

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (!File.Exists(SaveData.Instance.savePath()))
        {
            SaveData.Instance.CreateDefaultSaveFile(chara1);
            SaveData.Instance.XmlReader(chara2, "Health");
        } else
        {
            SaveData.Instance.XmlReader(chara2, "Health");
        }
        int levelReached1 = SaveData.Instance.levelLoadData(chara1);
        int levelReached2 = SaveData.Instance.levelLoadData(chara2);
        for (int i = 0; i < levelbuttons1.Length; i++)
        {
            if (i > levelReached1)
            {
                levelbuttons1[i].interactable = false;
            }
        }
        for (int i = 0; i < levelbuttons2.Length; i++)
        {
            if (i+1 > levelReached2)
            {
                levelbuttons2[i].interactable = false;
            }
        }
    }
    public void LoadLevelPassed(string level)
    {
        SceneManager.LoadScene(level);
    }
}
