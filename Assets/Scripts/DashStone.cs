using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashStone : MonoBehaviour
{
    public Animator animator;
    public Animator hudAnimator;

    public void GetDash()
    {
        animator.SetTrigger("GetDash");
        hudAnimator.SetTrigger("GetDash");
    }
}
