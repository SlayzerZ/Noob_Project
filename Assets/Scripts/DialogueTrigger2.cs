using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger2 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer button;
    //public Dialogue dialogue;
    private bool isInRange;
    public string PNJName;
    public string sentence;
    public Item[] ItemsToSell;

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            button.enabled = true;
        } else
        {
            button.enabled = false;
        }
        if (isInRange && PlayerController.Instance.movement.Movement.Dialogue.IsPressed())
        {
            if (!DialogueManager.Instance.dialogueStart)
            {
               TriggerDialogue();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Turn(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            ShopManager.Instance.CloseShop();
        }
    }

    void TriggerDialogue()
    {
        ShopManager.Instance.OpenShop(ItemsToSell,PNJName,sentence);
    }

    private void Turn(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x - transform.position.x > 0)
            {
                if (!spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = true;
                }
            }
            else if (collision.transform.position.x - transform.position.x < 0)
            {
                if (spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
    }
}
