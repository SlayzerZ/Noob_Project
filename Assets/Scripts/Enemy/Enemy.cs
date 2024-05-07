using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
    public SpriteRenderer spriteRenderer;
    public int damage = 10;

    public Enemy(float speed, SpriteRenderer spriteRenderer, int damage)
    {
        this.speed = speed;
        this.spriteRenderer = spriteRenderer;
        this.damage = damage;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
    }
}
