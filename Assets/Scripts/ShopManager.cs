using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text Name;
    public Text Dialogue;
    public Animator Animator;
    public GameObject ButtonPrefab;
    public Transform cellButtonParent;
    public static ShopManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il ya déjà une instance de Shop.");
            return;
        }
        Instance = this;
      //  sentences = new Queue<string>();
    }

    public void OpenShop(Item[] items, string pnjName, string sentence)
    {
        Name.text = pnjName;
        Dialogue.text = sentence;
        UpdateItems(items);
        Animator.SetBool("isOpen", true);
    }

    void UpdateItems(Item[] items)
    {
        for (int i = 0; i < cellButtonParent.childCount; i++)
        {
            Destroy(cellButtonParent.GetChild(i).gameObject);
        }
        for (int i = 0; i < items.Length; i++)
        {
            GameObject button = Instantiate(ButtonPrefab, cellButtonParent);
            SellItemButton buttonScript = button.GetComponent<SellItemButton>();
            buttonScript.itemName.text = items[i].name;
            buttonScript.itemImage.sprite = items[i].icon;
            buttonScript.itemName.text = items[i].name;
            buttonScript.itemPrice.text =  items[i].price.ToString();
            buttonScript.item = items[i];
            button.GetComponent<Button>().onClick.AddListener(delegate { buttonScript.BuyItem(); }) ;
        }
    }

    public void CloseShop()
    {
        //dialogueStart = false;
        Animator.SetBool("isOpen", false);
    }

    internal void OpenShop(List<Item> itemsToSell, string pNJName)
    {
        throw new NotImplementedException();
    }
}
