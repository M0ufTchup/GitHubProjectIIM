using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBossAttackDamage : MonoBehaviour
{
    public float damage = 20f;
    public bool noHurtPlayer = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !noHurtPlayer)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        if (other.CompareTag("SoulBossSoul"))
        {
            other.GetComponent<AngrySoulMvm>().TakeDamage(30);
        }
        if (other.CompareTag("Ennemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(30);
        }
    }
}
