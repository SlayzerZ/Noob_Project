using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items")]
public class Item : ScriptableObject
{
    public int id;
    public new string name;
    public string description;
    public Sprite icon;
    public int hpGiven;
    public int speedGiven;
    public int lifeGiven;
    public float manaGiven;
    public float speedDuration;
    public int price;

    public virtual void effect() 
    {
        if (id == 1)
        {
            PlayerHealth.Instance.GainLife(lifeGiven);
        } else if (id == 2)
        {
            PlayerHealth.Instance.GainHealth(hpGiven);
        } else if (id == 3)
        {
            SpecialAttack.Instance.RegenMana(manaGiven);
        } else if (id == 4)
        {
            PlayerController.Instance.DoubleSpeed(speedDuration);
        }
        else
        {

        }

    }
}
