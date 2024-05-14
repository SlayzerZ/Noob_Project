using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Animator m_Animator;
    public float powerBounce;
    public AudioClip sound;
    private void Start()
    {
        m_Animator = transform.parent.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_Animator.SetTrigger("Bounce");
            AudioManager.Instance.playAtPoint(sound,transform.position);
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * powerBounce);
        }
    }
}
