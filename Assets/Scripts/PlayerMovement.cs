using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float lowSpeed = 6f;
    public float dashSpeed = 25f;
    public float lowDashSpeed = 20f;

    [HideInInspector] public bool isDashing;
    public bool canDash;

    public Rigidbody2D rb;
    public Animator animator;
    IEnumerator dashCoroutine;
    IEnumerator attackCoroutine;

    public bool isAttacking;
    public bool canAttack;
    public bool range;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private Vector2 movement;
    Vector2 lastMove;

    [SerializeField] Transform bow;
    [SerializeField] GameObject ObjectBow;

    public GameObject body;
    public bool started = false;
    public string wich = "none";
    public float attack = 0;
    public bool dead;
    public bool stop;

    void Start()
    {
        canDash = false;
    }

    void Update()
    {
        AttackDamage[] Attack = GetComponentsInChildren<AttackDamage>();
        foreach (AttackDamage child in Attack)
        {
            child.attackDamage = attack;
        }

        if (!dead && !stop)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        if (movement.x != 0 || movement.y != 0)
            lastMove = movement;

            animator.SetFloat("Horizontal", lastMove.x);
            animator.SetFloat("Vertical", lastMove.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

        if (started)
        {
            body.GetComponent<BodyChangeStats>().animator.SetFloat("Horizontal", lastMove.x);
            body.GetComponent<BodyChangeStats>().animator.SetFloat("Vertical", lastMove.y);
            body.GetComponent<BodyChangeStats>().animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        RotateBow();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true && !isAttacking && !dead && !stop)
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(0.2f, 0.2f);
            StartCoroutine(dashCoroutine);
        }

        if(Time.time >= nextAttackTime && !range && canAttack && !dead && !stop)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !isDashing)
            {
                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                }
                attackCoroutine = Attacked(0.13f);
                StartCoroutine(attackCoroutine);
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }
    }

    private void FixedUpdate()
    {
        if (!(movement.x != 0 && movement.y != 0))
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position + movement * lowSpeed * Time.fixedDeltaTime);
        if (isDashing && !(movement.x != 0 && movement.y != 0))
        {
            rb.MovePosition(rb.position + lastMove * dashSpeed * Time.fixedDeltaTime);
        }
        else if (isDashing && (movement.x != 0 && movement.y != 0))
            rb.MovePosition(rb.position + lastMove * lowDashSpeed * Time.fixedDeltaTime);

    }


    public void GetDash()
    {
        canDash = true;
    }

    private void RotateBow()
    {
        float angle = Utility.AngleTowardsMouse(bow.position);
        bow.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));


    }

    public void GoToSpawn()
    {
        transform.position = new Vector3(22.5f,-43.5f, -1);
    }
    IEnumerator Dash(float dashDuration, float dashCooldown)
    {
        isDashing = true;
        animator.SetBool("Dash", true);
        if (wich != "SoulBoss")
            body.GetComponent<BodyChangeStats>().animator.SetBool("Dash", true);
        else
            body.GetComponent<BodyChangeStats>().animator.SetFloat("Dash", 1);
        GetComponent<Collider2D>().enabled = false;
        canDash = false;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        animator.SetBool("Dash", false);
        if (wich != "SoulBoss")
            body.GetComponent<BodyChangeStats>().animator.SetBool("Dash", false);
        else
            body.GetComponent<BodyChangeStats>().animator.SetFloat("Dash", 0);
        GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    IEnumerator Attacked(float attackDuration)
    {
        if (body != null)
            body.GetComponent<BodyChangeStats>().animator.SetTrigger("Attack");
        if (wich == "Brown" || wich == "White")
            animator.SetTrigger("SmallA");
        if (wich == "Green")
            animator.SetTrigger("BigA");
        if (wich == "Red" || wich == "Orange")
            animator.SetTrigger("MedA");
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    public void StopOn()
    {
        stop = true;
    }

    public void StopOff()
    {
        stop = false;
    }
}
