using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Rigidbody2D rb;
    public SpriteRenderer sr;

    private Vector2 direction;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private float LastX;
    private float X;

    void Start()
    {
        waitTime = startWaitTime;

        X = Random.Range(minX, maxX);
        direction = new Vector2(X, Random.Range(minY, maxY));
        LastX = X;
    }

    private void FixedUpdate()
    {
        if (LastX > X)
            sr.flipY = true;
        else if (LastX < X)
            sr.flipY = false;

        transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);

        Vector2 lookDir = direction - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;


        if (Vector2.Distance(transform.position, direction) < 0.2f)
        {
            if (waitTime <= 0)
            {
                LastX = X;
                X = Random.Range(minX, maxX);
                direction = new Vector2(X, Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
