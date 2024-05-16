using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text Name;
    public Text Dialogue;
    private Queue<string> sentences;
    public Animator Animator;
    [HideInInspector] public bool dialogueStart = false;

    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il ya déjà une instance de Dialogue.");
            return;
        }
        Instance = this;
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueStart = true;
        Animator.SetBool("isOpen", dialogueStart);
        Name.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        Dialogue.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            Dialogue.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void EndDialogue()
    {
        dialogueStart = false;
        Animator.SetBool("isOpen", dialogueStart);
    }
}
