using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmyHealth : PlayerHealth
{
    public AmyHealth(int maxHealth, int currentHealth, float invicibilityTime, float invicibilityFlashDelay, int maxLife, int startLife, int currentLife, Bar healthBar, SpriteRenderer graphics, float velocity, bool ground, bool isInvincible, Animator animator) : base(maxHealth, currentHealth, invicibilityTime, invicibilityFlashDelay, maxLife, startLife, currentLife, healthBar, graphics, velocity, ground, isInvincible, animator)
    {
    }

    public override void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            if (currentHealth - damage <= 0)
            {
                AudioManager.Instance.playAtPoint(deathSound, transform.position);
                currentHealth = 0;
            }
            else
            {
                rd.AddForce(new Vector2(-transform.localScale.x, 300));
                AudioManager.Instance.playAtPoint(damageSound, transform.position);
                currentHealth -= damage;
            }
            healthBar.setHealth(currentHealth);

            if (currentHealth == 0)
            {
                Die();
                return;
            }
            animator.SetTrigger("Damage");
            isInvincible = true;
            StartCoroutine(invicibilityFlash());
            StartCoroutine(HandleInvicinbile());
            StartCoroutine(DisableSpeed());
        }
    }
}
