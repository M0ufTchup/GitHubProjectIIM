using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    public float attackDamage = 10;
    public Animator animator;
    public Animator hudAnimator;
    public GameObject player;
    public GameObject Heart;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GetDash"))
        {
            player.GetComponent<PlayerMovement>().GetDash();
            animator.SetTrigger("GetDash");
            hudAnimator.SetTrigger("GetDash");
            other.GetComponent<DashStone>().GetDash();
        }
        if (other.CompareTag("Ennemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
        if(other.CompareTag("Flowey"))
        {
            other.GetComponent<FloweyHealth>().TakeDamage(attackDamage);
        }
        if (other.CompareTag("FloweyHeart"))
        {
            Heart.GetComponent<FloweyHealth>().TakeDamage(attackDamage * 2);
        }
        if (other.CompareTag("SoulBoss"))
        {
            other.GetComponent<SoulBossMovement>().Hurt();
            other.GetComponent<SoulBossHealth>().TakeDamage(attackDamage);
        }
        if (other.CompareTag("SoulBossSoul"))
        {
            other.GetComponent<AngrySoulMvm>().TakeDamage(attackDamage);
        }
    }
}
