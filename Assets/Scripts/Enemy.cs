using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public float maxHealth = 100;
    public float attackDamage = 10;
    public bool animationFinished = false;
    private float currentHealth;
    public bool nonEnnemy;
    public GameObject xpSpawn;
    public GameObject whereToSpawn;
    public int xpMin;
    public int xpMax;
    private int randX;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (animationFinished)
            Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
        else
            animator.SetTrigger("Hurt");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !nonEnnemy)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
    
    void Die()
    {
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;

        randX = Random.Range(xpMin, xpMax);
        for (int i = 0; i < randX; i++)
        {
            xpSpawn = Instantiate(xpSpawn, whereToSpawn.transform.position, Quaternion.identity);
            xpSpawn.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200,  200), Random.Range(-200, 200)));
        }
    }
}
