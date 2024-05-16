using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator animator;
    public AudioClip check;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Checkpoint");
            AudioManager.Instance.playAtPoint(check,transform.position);
            LevelManager.Instance.respawnPoint = transform.position;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}
