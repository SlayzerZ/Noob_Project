using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSonic : PlayerHealth
{
    public PlayerHealthSonic(int maxHealth, int currentHealth, float invicibilityTime, float invicibilityFlashDelay, HealthBar healthBar,
    SpriteRenderer graphics, float velocity, bool ground, bool isInvincible) : base(maxHealth, currentHealth, invicibilityTime, invicibilityFlashDelay, healthBar, graphics,velocity,ground,isInvincible) { }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
       // Debug.Log(ground);
    }
}
