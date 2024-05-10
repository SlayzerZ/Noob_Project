using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHealth : EnemyHealth
{
    public GameObject obj;
    public MinionHealth(int maxHealth, int currentHealth) : base(maxHealth, currentHealth)
    {
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHealth == 0)
        {
            Destroy(obj);
        }
    }
}
