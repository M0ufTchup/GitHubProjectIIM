using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBarrier : MonoBehaviour
{
    [SerializeField] GameObject barrier;
    public Animator animator;

    public void EnableBarrier()
    {
        animator.SetTrigger("Appear");
        barrier.SetActive(true);
    }

    public void DisableBarrier()
    {
        animator.SetTrigger("Deseapear");
        barrier.SetActive(false);
    }
}
