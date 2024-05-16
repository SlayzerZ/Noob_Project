using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public Text coinsCountsText;

    public static Inventory Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il ya d�j� une instance.");
            return;
        }
        Instance = this;
    }

    public void AddCoins(int coins)
    {
        coinsCount += coins;
        Updateui();
        LevelManager.Instance.coinsPickup += coins;
    }

    public void RemoveCoins(int coins)
    {
        coinsCount -= coins;
        Updateui();
    }

    public void Updateui()
    {
        coinsCountsText.text = coinsCount.ToString();
    }
}
