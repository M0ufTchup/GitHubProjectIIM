using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNoTurn : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public Animator animator;
    private bool start;

    private Vector2 direction;
    private bool canAnim = false;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        start = false;

        waitTime = startWaitTime;

        direction = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    public void started(bool end)
    {
        start = end;
    }

    private void FixedUpdate()
    {
        if (start) 
        { 
            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);


            if (Vector2.Distance(transform.position, direction) < 0.2f)
            {
                if (waitTime <= 0)
               {
                    canAnim = true;
                   direction = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                   waitTime = startWaitTime;
               }
               else
               {
                 if (canAnim)
                  {
                      animator.SetTrigger("Stop");
                      canAnim = false;
                   }
                  waitTime -= Time.fixedDeltaTime;
              }
            }
        }
    }
}
