using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainHealth : MonoBehaviour
{
    public int healthPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && gameObject.CompareTag("Rings"))
        {
            PlayerHealth.Instance.GainHealth(healthPoint);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("Rings"))
        {
            PlayerHealth.Instance.GainHealth(healthPoint);
        }
    }
}
