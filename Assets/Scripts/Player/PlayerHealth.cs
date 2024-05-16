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
    public int currentLife = 1;
    public Bar healthBar;
    public LifeCount lifeCount;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public SpriteRenderer graphics;
    [HideInInspector] public bool isInvincible = false;
    protected float velocity;
    protected bool ground;
    protected Animator animator;
    private Animator fadeSys;
    private Rigidbody2D rd;

    public PlayerHealth(int maxHealth, int currentHealth, float invicibilityTime, float invicibilityFlashDelay, int maxLife, int startLife, int currentLife, Bar healthBar, SpriteRenderer graphics, float velocity, bool ground, bool isInvincible, Animator animator)
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
        this.animator = animator;
    }

    public static PlayerHealth Instance;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
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
        healthBar.setMaxHealth(maxHealth);
        velocity = GetComponent<PlayerController>().speed;
        animator = GetComponent<Animator>();
        animator.SetInteger("Life", currentLife-1);
        currentLife = startLife;
        lifeCount.setLife(currentLife);
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
                AudioManager.Instance.playAtPoint(deathSound,transform.position);
                currentHealth = 0;
            } else
            {
                rd.AddForce(new Vector2(-transform.localScale.x,600));
                AudioManager.Instance.playAtPoint(damageSound, transform.position);
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
        PlayerController.Instance.bCol2d.enabled = false;
        PlayerController.Instance.rd.bodyType = RigidbodyType2D.Kinematic;
        PlayerController.Instance.rd.velocity = Vector2.zero;
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
        yield return new WaitForSeconds(1.5f);
        if (!GetComponent<PlayerController>().Grounded())
        {
            animator.SetTrigger("DamageAir");
        }
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
        transform.position = LevelManager.Instance.respawnPoint;
    }

}
