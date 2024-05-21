using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaisseM : MonoBehaviour
{
    public float Mana;
    public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.playAtPoint(sound, transform.position);
            SpecialAttack.Instance.RegenMana(Mana);
            Destroy(gameObject);
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 700);
        }
    }
}
