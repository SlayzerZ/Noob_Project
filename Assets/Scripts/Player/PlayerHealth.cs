using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public float invicibilityTime = 3f;
    public float invicibilityFlashDelay = 0.2f;
    public HealthBar healthBar;
    protected bool isInvincible = false;
    public SpriteRenderer graphics;
    protected float velocity;
    protected bool ground;
    private Animator animator;

    public PlayerHealth(int maxHealth, int currentHealth, float invicibilityTime, float invicibilityFlashDelay, HealthBar healthBar, SpriteRenderer graphics, float velocity, bool ground, bool isInvincible)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.invicibilityTime = invicibilityTime;
        this.invicibilityFlashDelay = invicibilityFlashDelay;
        this.healthBar = healthBar;
        this.graphics = graphics;
        this.velocity = velocity;
        this.ground = ground;
        this.isInvincible = isInvincible;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        velocity = GetComponent<PlayerController>().speed;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ground = GetComponent<PlayerController>().Grounded();
    }

   public virtual void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            if (currentHealth - damage <= 0)
            {
                currentHealth = 0;
            } else
            {
                currentHealth -= damage;
            }
            healthBar.setHealth(currentHealth);
            animator.SetTrigger("Damage 0");
            isInvincible=true;
            StartCoroutine(invicibilityFlash());
            StartCoroutine(HandleInvicinbile());
            StartCoroutine(DisableSpeed());
        }
    }

    public IEnumerator invicibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicinbile()
    {
        yield return new WaitForSeconds(invicibilityTime);
        isInvincible = false ;
    }

    public IEnumerator DisableSpeed()
    {
        GetComponent<PlayerController>().speed = 0;
        yield return new WaitForSeconds(1f);
        GetComponent<PlayerController>().speed = velocity;
    }

}
