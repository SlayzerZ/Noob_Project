using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && gameObject.CompareTag("Rings"))
        {
            Inventory.Instance.AddCoins(1);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("Rings"))
        {
            Inventory.Instance.AddCoins(1);
            Destroy(gameObject);
        }
    }
}
