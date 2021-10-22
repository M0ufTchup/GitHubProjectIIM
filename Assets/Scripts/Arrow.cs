using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public float ArrowVelocity;
    [HideInInspector] public float ArrowDamage;
    [HideInInspector] public string wich = "none";

    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * ArrowVelocity;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (wich == "Purple")
        {
            if (ArrowDamage <= 5)
                ArrowDamage = 5;
            if (ArrowDamage > 5 && ArrowDamage <= 10)
                ArrowDamage = 10;
            if (ArrowDamage > 10 && ArrowDamage <= 15)
                ArrowDamage = 15;
            if (ArrowDamage > 15)
                ArrowDamage = 20;
        }
        else if (wich == "Dark")
        {
            if (ArrowDamage <= 5)
                ArrowDamage = 5;
            if (ArrowDamage > 5 && ArrowDamage <= 10)
                ArrowDamage = 10;
            if (ArrowDamage > 10 && ArrowDamage < 15)
                ArrowDamage = 20;
            else if (ArrowDamage >= 15)
                ArrowDamage = 50;
        }
        if (other.CompareTag("Ennemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(ArrowDamage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Flowey"))
        {
            other.GetComponent<FloweyHealth>().TakeDamage(ArrowDamage);
            Destroy(gameObject);
        }
        if (other.CompareTag("FloweyHeart"))
        {
            other.GetComponentInParent<FloweyHealth>().TakeDamage(ArrowDamage * 10);
            Destroy(gameObject);
        }
        if (other.CompareTag("SoulBoss"))
        {
            other.GetComponent<SoulBossMovement>().Hurt();
            other.GetComponent<SoulBossHealth>().TakeDamage(ArrowDamage);
            Destroy(gameObject);
        }
        if (other.CompareTag("SoulBossSoul"))
        {
            other.GetComponent<AngrySoulMvm>().TakeDamage(ArrowDamage);
            Destroy(gameObject);
        }
    }
}
