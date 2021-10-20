using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngrySoulMvm : MonoBehaviour
{
    private Animator animator;

    IEnumerator summonCoroutine;

    public GameObject armor;
    public GameObject player;
    public GameObject endHud;
    public Rigidbody2D rb;
    private CapsuleCollider2D hitbox;

    public float speed; 
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private float LastX;
    private float X;
    private Vector2 direction;
    private bool summoningLaunch = false;
    public bool summoning = false;
    private bool move = false;

    public bool inside = true;

    public float maxHealth = 600;
    public float currentHealth;
    private bool dead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        X = Random.Range(minX, maxX);
        direction = new Vector2(X, Random.Range(minY, maxY));
        LastX = X;
        currentHealth = maxHealth;
        hitbox = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (inside)
            transform.position = armor.transform.position;
        else if (!summoning && !dead)
        {
            if (!summoningLaunch)
            {
                if (summonCoroutine != null)
                {
                    StopCoroutine(summonCoroutine);
                }
                summonCoroutine = Summon();
                StartCoroutine(summonCoroutine);
                summoningLaunch = true;
            }
            move = true;

            if (Vector2.Distance(transform.position, direction) < 0.2f)
            {
                LastX = X;
                X = Random.Range(minX, maxX);
                direction = new Vector2(X, Random.Range(minY, maxY));
            }
        }
    }

    private void FixedUpdate()
    {
        if (move)
            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hurt");
        if (currentHealth > 0)
            currentHealth -= damage;
        else if (currentHealth <= 0)
        {
            dead = true; 
            move = false;
            if (summonCoroutine != null)
            {
                StopCoroutine(summonCoroutine);
            }
            if (armor.GetComponent<SoulBossMovement>().enabled == true)
            {
                armor.GetComponent<SoulBossMovement>().Down(false);
                armor.GetComponent<SoulBossMovement>().enabled = false;
            }
            armor.GetComponent<SoulBossHealth>().enabled = false;
            hitbox.enabled = false;
            animator.SetTrigger("Death");
        }
    }

    public void DestroySoul()
    {
        endHud.GetComponent<End>().Ending();
        Destroy(gameObject);
    }
    
    IEnumerator Summon()
    {
        if (!dead)
        {
            hitbox.enabled = true;
            armor.GetComponent<SoulBossMovement>().enabled = false;
            animator.SetTrigger("Out");
            yield return new WaitForSeconds(5f);
            move = false;
            if (player.GetComponent<PlayerMovement>().wich == "SoulBoss" && player.GetComponentInChildren<Possession>().inside == false)
            {
                armor.GetComponent<BodyChangeStats>().Rising(true);
                player.GetComponentInChildren<Possession>().summon = true;
            }
            hitbox.enabled = false;
            summoning = true;
            summoningLaunch = false;
            animator.SetTrigger("Summon");
            armor.GetComponent<SoulBossMovement>().enabled = true;
            armor.GetComponent<SoulBossMovement>().Up();
        }
    }
}
