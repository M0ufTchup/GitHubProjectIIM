using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Animator animator;
    public Animator npcAnimator;
    public GameObject npc;

    public GameObject firstSoul;
    public GameObject secondSoul;
    public GameObject thirdSoul;

    public GameObject shovelOne;
    public GameObject shovelTwo;
    public GameObject shovelThree;

    public GameObject corpseOne;
    public GameObject corpseTwo;
    public GameObject corpseThree;

    public GameObject holeOne;
    public GameObject holeTwo;
    public GameObject holeThree;

    public int soul = 0;

    IEnumerator CloseShop;

    public void FirstSoul()
    {
        if (soul >= 25)
        {
            FindObjectOfType<XpCollect>().shop(25);
            soul -= 25;
            animator.SetBool("isOpen", false);
            npcAnimator.SetTrigger("One");
            if (CloseShop != null)
            {
                StopCoroutine(CloseShop);
            }
            CloseShop = Close(firstSoul, shovelOne, 0.5f, 2f);
            StartCoroutine(CloseShop);
        }
        else
        {
            animator.SetTrigger("noBuyOne");
        }
    }

    public void SecondSoul()
    {

        if (soul >= 50)
        {
            FindObjectOfType<XpCollect>().shop(50);
            soul -= 50;
            animator.SetBool("isOpen", false);
            npcAnimator.SetTrigger("Two");
            if (CloseShop != null)
            {
                StopCoroutine(CloseShop);
            }
            CloseShop = Close(secondSoul, shovelTwo, 0.5f, 2f);
            StartCoroutine(CloseShop);
        }
        else
        {
            animator.SetTrigger("noBuyTwo");
        }
    }

    public void ThirdSoul()
    {

        if (soul >= 100)
        {
            FindObjectOfType<XpCollect>().shop(100);
            soul -= 100;
            animator.SetBool("isOpen", false);
            npcAnimator.SetTrigger("Three");
            if (CloseShop != null)
            {
                StopCoroutine(CloseShop);
            }
            CloseShop = Close(thirdSoul, shovelThree, 0.5f, 2f);
            StartCoroutine(CloseShop);
        }
        else
        {
            animator.SetTrigger("noBuyThree");
        }
    }

    public void closeShop()
    {
        animator.SetBool("isOpen", false);
        FindObjectOfType<PlayerMovement>().stop = false;
        FindObjectOfType<Possession>().no = false;
        npc.GetComponent<DialogueTrigger>().canSpeakAgain = true;
    }

    public void firstCorpse()
    {
        holeOne.SetActive(true);
        Instantiate(corpseOne, new Vector3(21.45f, -38.55f, -1f), Quaternion.identity);
    }

    public void secondCorpse()
    {
        holeTwo.SetActive(true);
        Instantiate(corpseTwo, new Vector3(23.84f, -37.39f, -1f), Quaternion.identity);
    }

    public void thirdCorpse()
    {
        holeThree.SetActive(true);
        Instantiate(corpseThree, new Vector3(26.76f, -38.97f, -1f), Quaternion.identity);
    }

    IEnumerator Close(GameObject soul, GameObject shovel, float cooldown, float stopCooldown)
    {
        yield return new WaitForSeconds(cooldown);
        shovel.SetActive(true);
        Destroy(soul);
        yield return new WaitForSeconds(stopCooldown);
        FindObjectOfType<PlayerMovement>().stop = false;
        FindObjectOfType<Possession>().no = false;
        npc.GetComponent<DialogueTrigger>().canSpeakAgain = true;
    }
}
