using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public Animator speakTextAnimator;
    public Animator shopAnimator;

    private Queue<string> sentences;

    private bool Speaking = false;
    [HideInInspector] public bool shop = false;

    public Color textcolor;

    void Start()
    {
     sentences = new Queue<string>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && Speaking == true)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Speaking = true;

        nameText.text = dialogue.name;

        //nameText.color = textcolor;

        animator.SetBool("isOpen", true);
        if (speakTextAnimator != null)
            speakTextAnimator.SetBool("isSpeaking", true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            if (!shop)
                EndDialogue();
            else
                OpenShop();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        Speaking = false;
        FindObjectOfType<DialogueTrigger>().canSpeakAgain = true;
        if (speakTextAnimator != null)
            speakTextAnimator.SetBool("isSpeaking", false);
        shop = false;
    }

    public void OpenShop()
    {
        animator.SetBool("isOpen", false);
        Speaking = false;
        if (speakTextAnimator != null)
            speakTextAnimator.SetBool("isSpeaking", false);
        shop = false;
        shopAnimator.SetBool("isOpen", true);
        FindObjectOfType<PlayerMovement>().stop = true;
        FindObjectOfType<Possession>().no = true;
    }
}
