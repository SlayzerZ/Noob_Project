using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private static string _folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SonicProject");
    private string _saveFilePath = Path.Combine(_folder, "SonicProject.xml");//path du fichier de Sauvegarde
   // private XElement? _XmlRoot;//Element <root/> du .xml
   // private XDocument loadedDoc;
    public static SaveData Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il ya d�j� une instance de SaveData.");
            return;
        }
        Instance = this;
    }
    public void CreateDefaultSaveFile(string chara)
    {
        Directory.CreateDirectory(_folder);
        XDocument saveFile = new(
            new XElement("Root",
            new XElement(chara,
            new XElement("Health", "100"),
            new XElement("Coins", Inventory.Instance.coinsCount),
            new XElement("Life", "3"),
            new XElement("Mana", SpecialAttack.Instance.currentMana),
            new XElement("LevelReached", 1)
            ))
        );
        saveFile.Save(this._saveFilePath);
    }
    public void saveData()
    {
        //PlayerPrefs.SetInt("coins",Inventory.Instance.coinsCount);
        XmlWriter(PlayerController.Instance.Name,"Health", PlayerHealth.Instance.currentHealth.ToString());
        XmlWriter(PlayerController.Instance.Name, "Coins", Inventory.Instance.coinsCount.ToString());
        XmlWriter(PlayerController.Instance.Name, "Life", PlayerHealth.Instance.currentLife.ToString());
        XmlWriter(PlayerController.Instance.Name, "Mana", SpecialAttack.Instance.currentMana.ToString());
    }

    public void levelSaveData(int level) 
    {
        XmlWriter(PlayerController.Instance.Name, "LevelReached", level.ToString());
    }

    public int levelLoadData(string chara)
    {
      return int.Parse(XmlReader(chara, "LevelReached"));
    }

    public void resetData()
    {
        //PlayerPrefs.SetInt("coins",Inventory.Instance.coinsCount);
        XmlWriter(PlayerController.Instance.Name, "Health", 100.ToString());
        XmlWriter(PlayerController.Instance.Name, "Coins", 0.ToString());
        XmlWriter(PlayerController.Instance.Name, "Life", 3.ToString());
        XmlWriter(PlayerController.Instance.Name, "Mana", 0.ToString());
    }

    public void resetSoftData()
    {
        //PlayerPrefs.SetInt("coins",Inventory.Instance.coinsCount);
        XmlWriter(PlayerController.Instance.Name, "Health", 100.ToString());
        XmlWriter(PlayerController.Instance.Name, "Life", 3.ToString());
    }

    public void loadData()
    {
       // PlayerHealth.Instance.currentHealth = int.Parse(XmlReader(PlayerController.Instance.Name, "Health"));
        Inventory.Instance.coinsCount = int.Parse(XmlReader(PlayerController.Instance.Name, "Coins"));
        PlayerHealth.Instance.currentLife = int.Parse(XmlReader(PlayerController.Instance.Name, "Life"));
        SpecialAttack.Instance.currentMana = float.Parse(XmlReader(PlayerController.Instance.Name, "Mana"));
       // PlayerHealth.Instance.healthBar.setHealth(PlayerHealth.Instance.currentHealth);
        Inventory.Instance.Updateui();
        PlayerHealth.Instance.lifeCount.setLife(PlayerHealth.Instance.currentLife);
        SpecialAttack.Instance.manaBar.setMana(SpecialAttack.Instance.currentMana);
    }

    public string XmlReader(string chara,string element)//Lecteur d'element xml
    {
        XElement XmlRoot = XDocument.Load(_saveFilePath).Element("Root").Element(chara);
        if (XmlRoot != null)//<root/> existe ?
        {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            XElement? Element = XmlRoot.Element(element);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            if (Element != null)//element existe ?
            {
                return Element.Value;
            }
        }
        return "0";
    }

    private void XmlWriter(string chara,string elementName, string valueToWrite)
    {
        XDocument _doc = XDocument.Load(this._saveFilePath);
        XElement XmlRoot = _doc.Descendants(chara).FirstOrDefault();
        if (XmlRoot != null) // <root/> existe ?
        {
            XElement element = _doc.Descendants(chara).Elements(elementName).FirstOrDefault();
            if (element != null) // L'�l�ment existe ?
            {
                element.SetValue(valueToWrite); // Mettez � jour la valeur de l'�l�ment
                _doc.Save(this._saveFilePath);
            }
            else // L'�l�ment n'existe pas
            {
                XElement newElement = new(elementName, valueToWrite);
                XmlRoot.Add(newElement); // Ajoutez le nouvel �l�ment sous le root
                _doc.Save(this._saveFilePath);
            }
        } else // L'�l�ment <root> n'existe pas
        {
            _doc.Root.Add(new XElement(chara,
            new XElement("Health", PlayerHealth.Instance.currentHealth),
            new XElement("Coins", Inventory.Instance.coinsCount),
            new XElement("Life", PlayerHealth.Instance.currentLife),
            new XElement("Mana", SpecialAttack.Instance.currentMana),
            new XElement("LevelReached", 1)
            ));
            _doc.Save(this._saveFilePath);
        }
    }

    public string savePath() {  return _saveFilePath; }
}

