using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boost;
    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            AudioManager.Instance.playAtPoint(sound,transform.position);
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.right * boost);
        }
    }
}
