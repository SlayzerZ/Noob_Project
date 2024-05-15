using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaisseR : MonoBehaviour
{
    public int rings;
    public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.playAtPoint(sound,transform.position);
            Inventory.Instance.AddCoins(rings);
            Destroy(gameObject);
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1500);
        }
    }
}
