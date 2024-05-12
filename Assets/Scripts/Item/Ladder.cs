using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool InRange;
    private GameObject player;
    private PlayerController playerController;
    private float JF;
    private bool add = false;
    public SpriteRenderer arrow;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        JF = playerController.jumpForce;
    }

    void Start()
    {
        arrow.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InRange && playerController.movement.Movement.Movement.ReadValue<Vector2>().y > 0)
        {
            playerController.IsClimbing = true;
            playerController.Jump = 0;
            playerController.setVy(0,0.2f);
            if (!add)
            {
                playerController.jumpForce *= 5;
                add = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InRange = true;
            arrow.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InRange = false;
            playerController.IsClimbing = false;
            playerController.jumpForce = JF;
            add = false;
            arrow.enabled = false;
        }
    }
}
