using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushDetect : MonoBehaviour
{

    public float chaseRadius;

    public Transform target;
    public Animator animator;
    public ParticleSystem particles;

    private bool isAttacking = false;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius && !isAttacking)
        {
            animator.SetTrigger("Attack");
            particles.Play();
            isAttacking = true;
        }
    }

   public void AlertObservers(string message)
    {
        if (message.Equals("AttackAnimationEnded"))
            isAttacking = false;
    }
}
