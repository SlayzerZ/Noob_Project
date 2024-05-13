using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ladder : MonoBehaviour
{
    private bool InRange;
    private GameObject player;
    private PlayerController playerController;
    private float JF;
    private bool add = false;
    public SpriteRenderer arrow;

    public UnityEvent resteAccrocher;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        JF = playerController.jumpForce2;
    }

    void Start()
    {
        arrow.enabled = false;
    }

   // bool excuteOnce = false;

    // Update is called once per frame
    void Update()
    {
        //if (excuteOnce) return;
        if (InRange && playerController.movement.Movement.Movement.ReadValue<Vector2>().y > 0)
        {
            playerController.IsClimbing = true;
            playerController.Jump = 0;
            playerController.setVy(0,0.2f);
            // Evenement Trigger enter et exit
            // Event in Ladder in et out
            // Condition du saut
          
            if (!add)
            {
                playerController.jumpForce2 *= 5;
              //  excuteOnce = false;
                add = true;
                resteAccrocher?.Invoke();
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
            playerController.jumpForce2 = JF;
            add = false;
            arrow.enabled = false;
           // excuteOnce = false;
        }
    }
}
