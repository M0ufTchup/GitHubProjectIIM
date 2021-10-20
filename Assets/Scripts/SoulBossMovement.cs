using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBossMovement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D myRigidBody;
    private ParticleSystem hurtParticle;
    private BoxCollider2D armorHitbox;

    IEnumerator patternCoroutine;
    IEnumerator dashCoroutine;
    IEnumerator prepareCoroutine;

    public GameObject target;
    public GameObject badSoul;
    public GameObject treeLeft;
    public GameObject treeRight;
    public float chaseSpawnRadius = 3f;
    public float chaseAttackRadius = 1f;

    public bool awake = false;
    
    public bool isDashing;
    public bool isPreparing;
    public bool isAttacking = false;
    public bool targetAnim = false;
    public bool isChasing = false;
    public bool canAttack = false;
    public bool playerDead = false;

    private float moveSpeed = 4f;
    private int randomAttack = 0;
    private Vector3 dashPosition;
    private bool waiting = false;
    private bool summoning = false;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        hurtParticle = GetComponent<ParticleSystem>();
        armorHitbox = GetComponent<BoxCollider2D>();
        armorHitbox.enabled = false;
    }

    void Update()
    {
        if (!awake)
            CheckDistanceSpawn();
        if (awake)
        {
            if (!isPreparing && !isDashing && !isAttacking && canAttack)
            {
                CheckDistanceAttack();
            }

            if (isPreparing)
            {
                isChasing = false;
                canAttack = false;
                changeAnim(target.transform.position - transform.position);
                animator.SetFloat("Rattack", 1); 
                if (dashCoroutine != null)
                {
                    StopCoroutine(dashCoroutine);
                }
                dashCoroutine = Dash();
                if (!playerDead)
                    StartCoroutine(dashCoroutine);
                isPreparing = false;
            }
        }

        if (targetAnim)
            changeAnim(target.transform.position - transform.position);
    }

    private void FixedUpdate()
    {
        if (isChasing && !playerDead)
        {
            if (!waiting)
            {
                if (prepareCoroutine != null)
                {
                    StopCoroutine(prepareCoroutine);
                }
                prepareCoroutine = Prepare();
                if (!playerDead)
                    StartCoroutine(prepareCoroutine);
                waiting = true;
            }
            Vector3 temp = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            changeAnim(temp - transform.position);
            myRigidBody.MovePosition(temp);
        }

        if (isDashing && !playerDead)
        {
            if (Vector2.Distance(dashPosition, transform.position) >= 0.1)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, dashPosition, moveSpeed * Time.deltaTime);
                myRigidBody.MovePosition(temp);
            }
            else
            {
                if (patternCoroutine != null)
                {
                    StopCoroutine(patternCoroutine);
                }
                patternCoroutine = Pattern(2);
                if (!playerDead)
                    StartCoroutine(patternCoroutine);
                isDashing = false;
                moveSpeed = 4f;
            }
        }

        if (summoning)
        {
            if (Vector2.Distance(badSoul.transform.position, transform.position) >= 0.1)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, badSoul.transform.position, 20 * Time.deltaTime);
                myRigidBody.MovePosition(temp);
            }
            else
            {
                target.GetComponentInChildren<Possession>().noboss();
                GetComponent<CapsuleCollider2D>().enabled = false;
                armorHitbox.enabled = true;
                badSoul.GetComponent<Animator>().SetTrigger("In");
                badSoul.GetComponent<AngrySoulMvm>().inside = true;
                badSoul.GetComponent<AngrySoulMvm>().summoning = false;
                GetComponent<SoulBossHealth>().currenthealth = 150;
                animator.SetBool("Summon", false);
                animator.SetFloat("Player", 0);
                if (patternCoroutine != null)
                {
                    StopCoroutine(patternCoroutine);
                }
                patternCoroutine = Pattern(1);
                if (!playerDead)
                    StartCoroutine(patternCoroutine);
                summoning = false;
            }
        }
    }

    void CheckDistanceSpawn()
    {
        if (Vector2.Distance(target.transform.position, transform.position) <= chaseSpawnRadius)
        {
            isChasing = false;
            animator.SetTrigger("Start");
            if (playerDead)
            {
                treeRight.GetComponent<ActiveBarrier>().EnableBarrier();
                playerDead = false;
            }
            waiting = false;
            treeLeft.GetComponent<ActiveBarrier>().EnableBarrier();
            awake = true;
        }
    }

    void CheckDistanceAttack()
    {
        if (Vector2.Distance(target.transform.position, transform.position) <= chaseAttackRadius && !isAttacking)
        {
            if (prepareCoroutine != null)
            {
                StopCoroutine(prepareCoroutine);
            }
            canAttack = false;
            isChasing = false;
            animator.SetTrigger("Attack");
            isAttacking = true;
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        animator.SetFloat("Horizontal", setVector.x);
        animator.SetFloat("Vertical", setVector.y);
    }

    private void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                SetAnimFloat(Vector2.right);
            else if (direction.x < 0)
                SetAnimFloat(Vector2.left);

        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
                SetAnimFloat(Vector2.up);
            else if (direction.y < 0)
                SetAnimFloat(Vector2.down);
        }
    }

    public void Hurt()
    {
        hurtParticle.Stop();
        hurtParticle.Play();
    }

    public void Started()
    {
        target.GetComponentInChildren<Possession>().noboss();
        canAttack = true;
        isChasing = true;
        armorHitbox.enabled = true;
    }

    public void Down(bool dead)
    {
        armorHitbox.enabled = false;
        isChasing = false;
        canAttack = false;
        isPreparing = false;
        isDashing = false;
        StopAllCoroutines();
        if (!dead)
        {
            armorHitbox.enabled = false;
            animator.SetTrigger("Down");
            moveSpeed = 4f;
            GetComponent<CapsuleCollider2D>().enabled = true;
            badSoul.GetComponent<AngrySoulMvm>().inside = false;
            animator.SetFloat("Rattack", 0);
            animator.SetFloat("DashAttack", 0);
            target.GetComponentInChildren<Possession>().yesboss();
        }
        else if (dead)
        {
            playerDead = true;
            if (patternCoroutine != null)
            {
                StopCoroutine(patternCoroutine);
            }
            patternCoroutine = Death();
            StartCoroutine(patternCoroutine);
        }
    }

    public void Up()
    {
        animator.SetBool("Summon", true);
        summoning = true;
    }

    IEnumerator Pattern(int range)
    {
        if (range == 2)
        {
            yield return new WaitForSeconds(0.5f);
            animator.SetFloat("Rattack", 0);
            animator.SetFloat("DashAttack", 0);
            yield return new WaitForSeconds(2f);
        }
        targetAnim = true;
        yield return new WaitForSeconds(1f);
        targetAnim = false;
        randomAttack = Random.Range(0, range);
        print(randomAttack);
        if (randomAttack < 2)
        {
            waiting = false;
            isChasing = true;
            isAttacking = false;
            canAttack = true;
        }
        else if (randomAttack == 2)
           isPreparing = true;
    }

    IEnumerator Dash()
    {
        targetAnim = true;
        yield return new WaitForSeconds(3);
        targetAnim = false;
        moveSpeed = 50f;
        isDashing = true;
        animator.SetFloat("DashAttack", 1);
        dashPosition = target.transform.position;
    }

    IEnumerator Prepare()
    {
        yield return new WaitForSeconds(5);
        isChasing = false;
        targetAnim = true;
        randomAttack = Random.Range(0, 4);
        print(randomAttack);
        if (randomAttack < 3)
        {
            targetAnim = false;
            isPreparing = true;
        }
        else if (randomAttack == 3)
        {
            yield return new WaitForSeconds(1.5f);
            waiting = false;
            isChasing = true;
            canAttack = true;
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("Rattack", 0);
        animator.SetFloat("DashAttack", 0);
        yield return new WaitForSeconds(2f);
        transform.position = new Vector3(-9.44f, -53.62f);
        treeLeft.GetComponent<ActiveBarrier>().DisableBarrier();
        treeRight.GetComponent<ActiveBarrier>().DisableBarrier();
        armorHitbox.enabled = false;
        animator.SetTrigger("Down");
        moveSpeed = 4f;
        awake = false;
        isAttacking = false;
        GetComponent<SoulBossHealth>().currenthealth = 150;
        badSoul.GetComponent<Animator>().SetTrigger("In");
        badSoul.GetComponent<AngrySoulMvm>().inside = true;
        badSoul.GetComponent<AngrySoulMvm>().summoning = false;
        badSoul.GetComponent<AngrySoulMvm>().currentHealth = 500;
        animator.SetFloat("Rattack", 0);
        animator.SetFloat("DashAttack", 0);
    }
}
