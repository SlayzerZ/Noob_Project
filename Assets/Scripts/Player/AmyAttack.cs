using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmyAttack : SpecialAttack
{
    public float ManaSGA;
    public float ManaUGA;
    public float ManaDGA;
    public float ManaSAA;
    public float ManaUAA;
    public float ManaDAA;
    public float amount;
    private AmyController controller;
    private AmyHealth Amyhealth;
    public AudioClip attacksound;
    private CircleCollider2D circleCollider;
    public AmyAttack(float maxMana, float currentMana, Bar manaBar, float laserLength, float laserRadius, float attackDelay, LayerMask layerMask, Animator anim, BoxCollider2D bCol2d, float velocity, float JF, bool attack) : base(maxMana, currentMana, manaBar, laserLength, laserRadius, attackDelay, layerMask, anim, bCol2d, velocity, JF, attack)
    {
    }

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<AmyController>();
        Amyhealth = GetComponent<AmyHealth>();
        controller.mapAmy.Platform.Special.performed += SpecialAttack;
        circleCollider = GetComponentInChildren<CircleCollider2D>();
    }

    void SpecialAttack(InputAction.CallbackContext obj)
    {
        float x = controller.movement.Movement.Movement.ReadValue<Vector2>().x;
        float y = controller.movement.Movement.Movement.ReadValue<Vector2>().y;
        if (controller.Grounded())
        {
            if (x != 0 && y == 0)
            {
                if (currentMana >= ManaSGA)
                {
                    StartCoroutine(SideAttackGround());
                    StartCoroutine(sideAttackGround());
                    DrainMana(ManaSGA);
                }
            }
            else if (y > 0 && x == 0)
            {
                if (currentMana >= ManaUGA)
                {
                  //  StartCoroutine(UpAttackGround());
                  //  StartCoroutine(upAttackGround());
                }
            }
            else if (y < 0 && x == 0)
            {
                if (currentMana >= ManaDGA)
                {
                    StartCoroutine(DownAttackGround());
                    StartCoroutine(downAttackGround());
                    DrainMana(ManaDGA);
                }
            }
        }
        else
        {
            if (x != 0 && y == 0)
            {
                if (currentMana >= ManaSAA)
                {
                    StartCoroutine(SideAttackAerial());
                    StartCoroutine(sideAttackAerial());
                    DrainMana(ManaSAA);
                }
            }
            else if (y > 0 && x == 0)
            {
                if (currentMana >= ManaUAA)
                {
                    UpAttackAerial();
                    DrainMana(ManaUAA);
                }
            }
            else if (y < 0 && x == 0)
            {
                if (currentMana >= ManaDAA)
                {
                    StartCoroutine(DownAttackAerial());
                    DrainMana(ManaDAA);
                }
            }
        }
    }

    void SGAtouch()
    {
        //Start point of the laser
        float limit;
        Vector2 direction;
        if (transform.localScale.x >= 0)
        {
            limit = -bCol2d.bounds.extents.x;
            direction = Vector2.right;
        }
        else
        {
            limit = bCol2d.bounds.extents.x;
            direction = Vector2.left;
        }
        Vector2 startPosition = (Vector2)transform.position - new Vector2(limit, 0);
        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, laserLength, layerMask.value);
        //The color of the ray for debug purpose
        Color rayColor = Color.red;
        //If the object is not null
        if (hit.collider != null)
        {
            //Change the color of the ray for debug purpose
            rayColor = Color.yellow;
            hit.collider.transform.GetComponent<EnemyHealth>().TakeDamage(50);
        }
        else
        {
            //Change the color of the ray for debug purpose
            rayColor = Color.blue;

        }
        //Draw the ray for debug purpose
        Debug.DrawRay(startPosition, direction * laserLength, rayColor);
    }
    private IEnumerator SideAttackGround()
    {
        anim.SetBool("SideGroundAttack", true);
        controller.speed = 0;
        controller.jumpForce = 0;
        attack = true;
        AudioManager.Instance.playAtPoint(attacksound, transform.position);
        while (attack)
        {
            SGAtouch();
            yield return new WaitForSeconds(attackDelay);
        }
        anim.SetBool("SideGroundAttack", false);
        controller.speed = velocity;
        controller.jumpForce = JF;
    }
    private IEnumerator sideAttackGround()
    {
        yield return new WaitForSeconds(3f);
        attack = false;
    }

    private IEnumerator DownAttackGround()
    {
        anim.SetBool("DownGroundAttack", true);
        controller.speed = 0;
        attack = true;
        PlayerController.Instance.setVy(0, 0);
        AudioManager.Instance.playAtPoint(attacksound, transform.position);
        yield return new WaitForSeconds(1f);
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(1500 * transform.localScale.x, 0));
        while (attack)
        {
            yield return new WaitForSeconds(attackDelay);
        }
        anim.SetBool("DownGroundAttack", false);
        controller.speed = velocity;
    }
    private IEnumerator downAttackGround()
    {
        yield return new WaitForSeconds(2f);
        attack = false;
    }

    private IEnumerator UpAttackGround()
    {
        anim.SetTrigger("UpGroundSpecial");
        controller.speed = 0;
        // controller.ss = true;
       // anim.SetBool("SS", controller.ss);
        //AudioManager.Instance.playAtPoint(attacksound, transform.position);
        yield return new WaitForSeconds(1f);
       // controller.mapAmy.Platform.Disable();
       // controller.mapSonic.SuperSonic.Enable();
        //  controller.rd.bodyType = RigidbodyType2D.Kinematic;
        controller.rd.gravityScale = 0;
       // healthSonic.isInvincible = true;
        controller.speed = velocity;
    }
    private IEnumerator upAttackGround()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            DrainMana(1);
            if (currentMana == 0)
            {
              //  controller.ss = false;
                Amyhealth.isInvincible = false;
             //   anim.SetBool("SS", controller.ss);
                controller.rd.gravityScale = 1;
              //  controller.mapSonic.SuperSonic.Disable();
             //   controller.mapAmy.Platform.Enable();
                break;
            }
        }
    }

    private IEnumerator SideAttackAerial()
    {
        anim.SetBool("SideAerialAttack", true);
        //anim.SetTrigger("SideAerialAttack");
        controller.jumpForce = 0;
        attack = true;
        AudioManager.Instance.playAtPoint(attacksound, transform.position);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * transform.localScale.x, 0));
        yield return new WaitForSeconds(1f);
        while (attack)
        {
            // SAAtouch(laserRadius+1, Vector3.zero);
            yield return new WaitForSeconds(attackDelay);
        }
        anim.SetBool("SideAerialAttack", false);
        controller.jumpForce = JF;
    }
    private IEnumerator sideAttackAerial()
    {
        yield return new WaitForSeconds(2f);
        attack = false;
    }

    private IEnumerator DownAttackAerial()
    {
        anim.SetTrigger("DownAerialSpecial");
        controller.speed = 0;
        controller.Jump = 1;
        AudioManager.Instance.playAtPoint(attacksound, transform.position);
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * amount);
        while (!controller.Grounded())
        {
            yield return new WaitForSeconds(attackDelay);
        }
        controller.speed = velocity;
    }

    private void UpAttackAerial()
    {
        anim.SetTrigger("UpAerialSpecial");
        AudioManager.Instance.playAtPoint(attacksound, transform.position);
        controller.Jump = 1;
       // yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * (controller.jumpForce * 2));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (circleCollider.enabled)
        {
            if (collision.gameObject.layer == 9)
            {
                collision.GetComponent<EnemyHealth>().TakeDamage(50);
            }
        }
    }
}

