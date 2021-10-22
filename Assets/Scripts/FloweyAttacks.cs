using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloweyAttacks : MonoBehaviour
{
    private float waitTime;
    public float startWaitTime;
    private bool start;

    public Rigidbody2D rb;
    public Transform target;
    public Animator animator;

    private Vector2 direction;

    void Start()
    {
        start = false;

        target = GameObject.FindWithTag("Player").transform;

        waitTime = startWaitTime;

        direction = target.position;
    }

    public void started(bool end)
    {
        start = end;
    }

    private void FixedUpdate()
    {
        if (start)
        {
            Vector2 lookDir = direction - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;

            if (waitTime <= 0)
            {
                direction = target.position;
                animator.SetTrigger("Attack");
                waitTime = Random.Range(4, 7);
            }
            else
            {
                waitTime -= Time.fixedDeltaTime;
            }
        }
    }
}
