using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public Animator animator;
    public Animator barAnimator;
    public Animator hudAnimator;
    public Animator pentaAnimator;
    public Animator npcAnimator;
    public ParticleSystem DeathParticle;

    public float maxHealth = 100;
    [HideInInspector] public float currentHealth;
    public bool hurt = false;
    public bool Invincible = true;

    public HealthBar healthBar;
    [HideInInspector] public GameObject body;
    public bool Out;
    public bool dead;

    public GameObject whereToSpawn;
    public GameObject corpse;
    public GameObject soulBossReset;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        if(body != null)
            healthBar.SetHealth(body.GetComponent<BodyChangeStats>().health);
    }

    public void TakeDamage(float damage)
    {
        if (Out)
        {
            Die();
            return;
        }
        BodyChangeStats Health = body.GetComponent<BodyChangeStats>();

        if (!hurt && !Out)
        {
            Debug.Log("PlayerTouched");

            Health.health -= damage;

            animator.SetTrigger("Hurt");
            Health.animator.SetTrigger("Hurt");
            barAnimator.SetTrigger("Hurt");

            hurt = true;
        }
        if (Health.health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!dead)
        {
            if (!Out)
                Instantiate(corpse, whereToSpawn.transform.position, Quaternion.identity);
            Out = true;
            soulBossReset.GetComponent<SoulBossMovement>().Down(true);
            FindObjectOfType<PlayerMovement>().dead = true;
            FindObjectOfType<Possession>().dead = true;
            animator.SetBool("IsDead", true);
            hudAnimator.SetTrigger("Death");
            pentaAnimator.SetTrigger("Summon");
            npcAnimator.SetTrigger("Summon");
            DeathParticle.Play();
            dead = true;
        }
    }

    public void Summon()
    {
        animator.SetBool("IsDead", false);
        dead = false;
        FindObjectOfType<PlayerMovement>().dead = false;
        FindObjectOfType<Possession>().dead = false;
    }

    public void RemoveallSouls()
    {
        FindObjectOfType<XpCollect>().DeadSouls();
    }
}
