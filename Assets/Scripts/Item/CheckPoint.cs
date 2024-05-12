using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Transform playerSpawn;
    private Animator animator;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerSpawn.childCount > 0)
            {
                for (int i = 0; i < playerSpawn.childCount; i++)
                {
                    Destroy(playerSpawn.GetChild(i).gameObject);
                }
            }
            animator.SetTrigger("Checkpoint");
            playerSpawn.position = transform.position;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}
