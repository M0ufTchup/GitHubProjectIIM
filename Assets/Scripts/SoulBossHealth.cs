using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBossHealth : MonoBehaviour
{
    private float maxHealth = 150;
    public float currenthealth = 0;

    void Start()
    {
        currenthealth = maxHealth;
    }

    void Update()
    {
        if (currenthealth <= 0)
        {
            GetComponent<SoulBossMovement>().Down(false);
            currenthealth = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currenthealth -= damage;
    }
}
