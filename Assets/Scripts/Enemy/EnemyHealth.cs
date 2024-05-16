using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;
    protected int currentHealth;
    

    public EnemyHealth(int maxHealth, int currentHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
            if (currentHealth - damage <= 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= damage;
            }
    }
}
