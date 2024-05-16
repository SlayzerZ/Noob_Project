using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAttack : MonoBehaviour
{
    public float maxMana;
    public float currentMana;
    public Bar manaBar;
    public float laserLength;
    public float laserRadius;
    public float attackDelay;
    public LayerMask layerMask;
    protected Animator anim;
    protected BoxCollider2D bCol2d;
    protected float velocity;
    protected float JF;
    protected bool attack = false;
    // Start is called before the first frame update

    public SpecialAttack(float maxMana, float currentMana, Bar manaBar,float laserLength, float laserRadius, float attackDelay, LayerMask layerMask, Animator anim, BoxCollider2D bCol2d, float velocity, float JF, bool attack)
    {
        this.maxMana = maxMana;
        this.currentMana = currentMana;
        this.manaBar = manaBar;
        this.laserLength = laserLength;
        this.laserRadius = laserRadius;
        this.attackDelay = attackDelay;
        this.layerMask = layerMask;
        this.anim = anim;
        this.bCol2d = bCol2d;
        this.velocity = velocity;
        this.JF = JF;
        this.attack = attack;
    }

    public static SpecialAttack Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il ya déjà une instance de SpecialAttack.");
            return;
        }
        Instance = this;
        manaBar.setMaxMana(maxMana);
    }
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        bCol2d = GetComponent<BoxCollider2D>();
        velocity = GetComponent<PlayerController>().speed;
        JF = GetComponent<PlayerController>().jumpForce;
        
    }

    public void DrainMana(float amount)
    {
        if (currentMana - amount < 0)
        {
            currentMana = 0;
        }
        else
        {
            currentMana -= amount;
        }
        manaBar.setMana(currentMana);
    }

    public void RegenMana(float amount)
    {
        if (currentMana + amount > maxMana)
        {
            currentMana = maxMana;
        }
        else
        {
            currentMana += amount;
        }
        manaBar.setMana(currentMana);
    }
}
