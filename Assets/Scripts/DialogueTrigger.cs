using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Transform target;

    public bool canSpeakAgain = true;
    public bool autoSpeak = false;
    public bool dontTrigger = false;
    public bool shop = false;

    private bool canSpeak = false;
    public float chaseRadius = 1.5f;

    public Color textcolor;

    IEnumerator speakCoroutine;

    private void Update()
    {
        if (canSpeak && Input.GetKeyDown(KeyCode.Space) && canSpeakAgain && !dontTrigger)
            TriggerDialogue();
        else
            CheckDistance();

        if (autoSpeak && canSpeak)
        {
            if (speakCoroutine != null)
            {
                StopCoroutine(speakCoroutine);
            }
            speakCoroutine = Speak(4f);
            StartCoroutine(speakCoroutine);
            autoSpeak = false;
        }
    }

    public void TriggerDialogue()
    {
        canSpeakAgain = false;
        canSpeak = false;
        if (shop)
            FindObjectOfType<DialogueManager>().shop = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<DialogueManager>().textcolor = textcolor;
    }

    void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius)
        {
            canSpeak = true;
        }
        else
        {
            canSpeak = false;
            if (canSpeakAgain == false)
                FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

    IEnumerator Speak(float checkDuration)
    {
        yield return new WaitForSeconds(checkDuration);
        TriggerDialogue();
    }
}
