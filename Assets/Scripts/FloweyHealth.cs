using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloweyHealth : MonoBehaviour
{
    public Animator animator;

    public float maxHealth = 100;
    private bool started;
    private float currentHealth;
    private bool attack;
    private bool open;
    private bool dead;

    public GameObject trapOne;
    public GameObject trapTwo;
    public GameObject trapThree;
    public GameObject mainAttack;


    IEnumerator petalLCoroutine;
    IEnumerator petalRCoroutine;
    IEnumerator startCoroutine;
    private float healthBeforeDown = 100;
    private bool petalLDown = true;
    private bool petalRDown = false;

    public GameObject xpSpawn;
    public GameObject whereToSpawn;
    public int xpMin;
    public int xpMax;
    private int howMuch = 50;

    void Start()
    {
        currentHealth = maxHealth;
        started = false;
        attack = false;
        open = false;
        dead = false;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("ennemy touch");

        currentHealth -= damage;
        if (healthBeforeDown > 0 && attack)
            healthBeforeDown -= damage;

        if (!open)
        {
            animator.SetTrigger("Open");
            if (startCoroutine != null)
            {
                StopCoroutine(startCoroutine);
            }
            startCoroutine = Starting(3f);
            StartCoroutine(startCoroutine);
            open = true;
        }

        if (attack)
        {
            if (currentHealth <= 0 && !dead)
            {
                animator.SetBool("Death", true);
                trapOne.GetComponent<PatrolNoTurn>().started(started);
                trapTwo.GetComponent<PatrolNoTurn>().started(started);
                trapThree.GetComponent<PatrolNoTurn>().started(started);
                mainAttack.GetComponent<FloweyAttacks>().started(started);
                for (int i = 0; i < howMuch; i++)
                {
                    xpSpawn = Instantiate(xpSpawn, whereToSpawn.transform.position, Quaternion.identity);
                    xpSpawn.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-400, 400), Random.Range(-400, 400)));
                }
                dead = true;
            }
            else if (!dead)
                animator.SetTrigger("Hurt");
        }
    }

    private void Update()
    {
        if (healthBeforeDown <= 0)
        {
            if (!petalRDown)
            {
                animator.SetBool("PrDown", true);
                if (petalRCoroutine != null)
                {
                    StopCoroutine(petalRCoroutine);
                }
                petalRCoroutine = PetalR(5f, 5f);
                StartCoroutine(petalRCoroutine);
                healthBeforeDown = 50;
            }
            else if (!petalLDown)
            {
                animator.SetBool("PlDown", true);
                if (petalRCoroutine != null)
                {
                    StopCoroutine(petalRCoroutine);
                }
                if (petalLCoroutine != null)
                {
                    StopCoroutine(petalLCoroutine);
                }
                petalLCoroutine = PetalL(5f);
                StartCoroutine(petalLCoroutine);
            }
        }
    }

    IEnumerator PetalR(float petalR, float petalBack)
    {
        petalRDown = true;
        petalLDown = false;
        yield return new WaitForSeconds(petalR);
        petalLDown = true;
        yield return new WaitForSeconds(petalBack);
        animator.SetBool("PrDown", false);
        petalRDown = false;
        healthBeforeDown = 50;
    }

    IEnumerator PetalL(float petalBack)
    {
        petalLDown = true;
        yield return new WaitForSeconds(petalBack);
        animator.SetBool("PrDown", false);
        animator.SetBool("PlDown", false);
        healthBeforeDown = 100;
        petalRDown = false;
    }

    IEnumerator Starting(float opening)
    {
        started = true;
        yield return new WaitForSeconds(opening);
        animator.SetTrigger("Start");
        trapOne.GetComponent<PatrolNoTurn>().started(started);
        trapTwo.GetComponent<PatrolNoTurn>().started(started);
        trapThree.GetComponent<PatrolNoTurn>().started(started);
        mainAttack.GetComponent<FloweyAttacks>().started(started);
        started = false;
        attack = true;
    }
}
