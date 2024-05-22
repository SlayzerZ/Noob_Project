using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItemButton : MonoBehaviour
{
    public Text itemName;
    public Image itemImage;
    public Text itemPrice;

    public Item item;

    public void BuyItem()
    {
        Inventory inventory = Inventory.Instance;
        if (inventory.coinsCount >= item.price)
        {
            inventory.RemoveCoins(item.price);
            item.effect();
        }
    }
}
