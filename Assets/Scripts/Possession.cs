using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    public GameObject player;
    public Animator barAnimator;

    public bool inside = true;
    public bool getDash = false;
    public bool noBoss = true;
    public bool dead = false;
    public bool no;
    public bool summon = false;

    IEnumerator possessCoroutine;

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.X) && !no) || (Input.GetKeyDown(KeyCode.Mouse1) && !no) || getDash || dead)
        {
            if (possessCoroutine != null)
            {
                StopCoroutine(possessCoroutine);
            }
            possessCoroutine = Possess(0.05f);
            StartCoroutine(possessCoroutine);
            getDash = false;
            if (dead)
                noBoss = true;
            dead = false;
        }
        if (summon && !noBoss)
        {
            if (inside)
            {
                player.GetComponent<PlayerMovement>().animator.SetBool("Inside", inside);
                player.GetComponent<PlayerMovement>().started = inside;
                barAnimator.SetTrigger("Appear");
                inside = false;
            }
            else
            {
                player.GetComponent<PlayerMovement>().animator.SetBool("Inside", inside);
                player.GetComponent<PlayerMovement>().started = inside;
                barAnimator.SetTrigger("Deseapear");
                inside = true;
            }
            player.GetComponent<PlayerHealth>().Out = inside;
            summon = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cadaver"))
        {
            if (inside)
            {
                player.GetComponent<PlayerMovement>().animator.SetBool("Inside", inside);
                player.GetComponent<PlayerMovement>().started = inside;
                barAnimator.SetTrigger("Appear");
                inside = false;
            }
            else
            {
                player.GetComponent<PlayerMovement>().animator.SetBool("Inside", inside);
                player.GetComponent<PlayerMovement>().started = inside;
                barAnimator.SetTrigger("Deseapear");
                inside = true;
            }
            player.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, -1);
            other.GetComponent<BodyChangeStats>().Rising(inside);
            player.GetComponent<PlayerHealth>().Out = inside;
        }
        if (other.CompareTag("SoulBoss") && !noBoss)
        {
            if (inside)
            {
                player.GetComponent<PlayerMovement>().animator.SetBool("Inside", inside);
                player.GetComponent<PlayerMovement>().started = inside;
                barAnimator.SetTrigger("Appear");
                inside = false;
            }
            else
            {
                player.GetComponent<PlayerMovement>().animator.SetBool("Inside", inside);
                player.GetComponent<PlayerMovement>().started = inside;
                barAnimator.SetTrigger("Deseapear");
                inside = true;
            }
            other.GetComponent<Animator>().SetFloat("Player", 1);
            player.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, -1);
            other.GetComponent<BodyChangeStats>().Rising(inside);
            player.GetComponent<PlayerHealth>().Out = inside;
        }
    }

    public void noboss()
    {
        noBoss = true;
    }

    public void yesboss()
    {
        noBoss = false;
    }

    IEnumerator Possess(float checkDuration)
    {
        player.GetComponentInChildren<Possession>().GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(checkDuration);
        player.GetComponentInChildren<Possession>().GetComponent<BoxCollider2D>().enabled = false;
    }
}
