using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeebotPatrol : Enemy
{
    public Transform[] waypoints;
    private Transform target;
    private int destPoints = 0;
    private bool playerDetected;
    private Animator animator;

    public BeebotPatrol(float speed, SpriteRenderer spriteRenderer, int damage) : base(speed, spriteRenderer, damage)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime,Space.World);
        if (playerDetected)
        {
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                animator.SetTrigger("Attack");
            }
        } else
        {
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                destPoints = (destPoints + 1) % waypoints.Length;
                target = waypoints[destPoints];
                if (target.position.x - transform.position.x > 0)
                {
                    if (!spriteRenderer.flipX)
                    {
                        spriteRenderer.flipX = true;
                    }
                }
                else if (target.position.x - transform.position.x < 0)
                {
                    if (spriteRenderer.flipX)
                    {
                        spriteRenderer.flipX = false;
                    }
                }
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            target = collision.transform;
            if (collision.transform.position.x - transform.position.x > 0)
            {
                if (!spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = true;
                }
            }
            else if (collision.transform.position.x - transform.position.x < 0)
            {
                if (spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            target = waypoints[destPoints];
            if (target.position.x - transform.position.x > 0)
            {
                if (!spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = true;
                }
            }
            else if (target.position.x - transform.position.x < 0)
            {
                if (spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
    }
}
