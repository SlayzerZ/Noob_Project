using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSonic : PlayerHealth
{
    public PlayerHealthSonic(int maxHealth, int currentHealth, float invicibilityTime, float invicibilityFlashDelay, int maxLife, int startLife, int currentLife, Bar healthBar,
    SpriteRenderer graphics, float velocity, bool ground, bool isInvincible, Animator animator) : base(maxHealth, currentHealth, invicibilityTime, invicibilityFlashDelay, maxLife, startLife, currentLife, healthBar, graphics,velocity,ground,isInvincible, animator) { }

    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (!isInvincible)
        {
            animator.SetBool("SideAerialAttack", false);
        }
    }
}
