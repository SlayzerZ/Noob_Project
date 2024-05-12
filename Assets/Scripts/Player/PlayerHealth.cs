using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float invicibilityTime = 3f;
    public float invicibilityFlashDelay = 0.2f;
    public int maxLife = 9;
    public int startLife = 3;
    protected int currentLife = 1;
    public HealthBar healthBar;
    public LifeCount lifeCount;
    protected bool isInvincible = false;
    public SpriteRenderer graphics;
    protected float velocity;
    protected bool ground;
    private Animator animator;
    private Transform playerSpawn;
    private Animator fadeSys;

    public PlayerHealth(int maxHealth, int currentHealth, float invicibilityTime, float invicibilityFlashDelay, int maxLife, int startLife, int currentLife, HealthBar healthBar, SpriteRenderer graphics, float velocity, bool ground, bool isInvincible)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.invicibilityTime = invicibilityTime;
        this.invicibilityFlashDelay = invicibilityFlashDelay;
        this.maxLife = maxLife;
        this.startLife = startLife;
        this.currentLife = currentLife;
        this.healthBar = healthBar;
        this.graphics = graphics;
        this.velocity = velocity;
        this.ground = ground;
        this.isInvincible = isInvincible;
    }

    public static PlayerHealth Instance;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSys = GameObject.FindGameObjectWithTag("FadeSys").GetComponent<Animator>();
        if (Instance != null)
        {
            Debug.LogWarning("Il ya déjà une instance de PlayerHealth.");
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        currentLife = startLife;
        healthBar.setMaxHealth(maxHealth);
        lifeCount.setLife(currentLife);
        velocity = GetComponent<PlayerController>().speed;
        animator = GetComponent<Animator>();
        animator.SetInteger("Life", currentLife-1);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ground = PlayerController.Instance.Grounded();
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

            if (currentHealth == 0)
            {
                Die();
                return;
            }
            animator.SetTrigger("Damage 0");
            isInvincible = true;
            StartCoroutine(invicibilityFlash());
            StartCoroutine(HandleInvicinbile());
            StartCoroutine(DisableSpeed());
        }
   }

    public virtual void GainHealth(int heal)
    {
        if (currentHealth + heal >= maxHealth)
        {
            currentHealth = maxHealth;
        }else
        {
            currentHealth += heal;
        }
        healthBar.setHealth(currentHealth);
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        currentLife -= 1;
        lifeCount.setLife(currentLife);
        PlayerController.Instance.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        if (currentLife > 0)
        {
            StartCoroutine(ReplacePlayer());
        } else
        {
            GameOverManager.Instance.onPlayerDeath();
        }
    }

    public void Respawn()
    {
        currentLife = startLife;
        lifeCount.setLife(currentLife);
        PlayerController.Instance.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        animator.SetInteger("Life", currentLife - 1);
        healthBar.setMaxHealth(maxHealth);
        currentHealth = maxHealth;
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

    private IEnumerator ReplacePlayer()
    {
        fadeSys.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        PlayerController.Instance.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        animator.SetInteger("Life", currentLife-1);
        healthBar.setMaxHealth(maxHealth);
        currentHealth = maxHealth;
        transform.position = playerSpawn.position;
    }

}
