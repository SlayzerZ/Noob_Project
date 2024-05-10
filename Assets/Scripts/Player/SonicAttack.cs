using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SonicAttack : MonoBehaviour
{
    public float laserLength;
    public float attackDelay;
    public LayerMask layerMask;
    private Animator anim;
    private SonicController controller;
    private BoxCollider2D bCol2d;
    private float velocity;
    private bool attack = false;
    // Start is called before the first frame update
    protected void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<SonicController>();
        bCol2d = GetComponent<BoxCollider2D>();
        velocity = controller.speed;
        controller.mapSonic.Platform.Special.performed += SpecialAttack;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpecialAttack(InputAction.CallbackContext obj)
    {
        float x = controller.mapSonic.Platform.Move.ReadValue<Vector2>().x;
        float y = controller.mapSonic.Platform.Move.ReadValue<Vector2>().y;
        if (controller.Grounded() && x != 0)
        {
            StartCoroutine(SideAttackGround());
            StartCoroutine(sideAttackGround());
        }
    }

    void SGAtouch() 
    {
        //Start point of the laser
        Vector2 startPosition = (Vector2)transform.position - new Vector2(-bCol2d.bounds.extents.x, 0);
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.right, laserLength, layerMask.value);
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
        Debug.DrawRay(startPosition, Vector2.right * laserLength, rayColor);
    }

    private IEnumerator SideAttackGround()
    {
        anim.SetBool("SideGroundAttack",true);
        controller.speed = 0;
        attack = true;
        while (attack)
        {
            SGAtouch();
            yield return new WaitForSeconds(attackDelay);
        }
        anim.SetBool("SideGroundAttack", false);
        controller.speed = velocity;
    }
    private IEnumerator sideAttackGround()
    {
        yield return new WaitForSeconds(2f);
        attack = false;
    }
}
