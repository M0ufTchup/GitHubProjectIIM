using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyChangeStats : MonoBehaviour
{
    public float attackRate;
    public float attackDamage;

    [Range(0, 15)]
    public float bowPower;
    [Range(0, 3)]
    public float maxBowCharge;
    public float arrowDamage;
    public float minimumRate;
    public bool range;

    public float health;
    public Color bar;

    public GameObject player;
    public GameObject bow;
    public Animator animator;
    public Image healtBar;

    public bool raised = false;
    [HideInInspector] public bool canRaise = false;
    public string wich = "none";

    IEnumerator deathCoroutine;


    private void Update()
    {
        if (raised)
            transform.position = player.transform.position;

        if (health <= 0)
        {
            if (deathCoroutine != null)
            {
                StopCoroutine(deathCoroutine);
            }
            deathCoroutine = Death(5f);
            StartCoroutine(deathCoroutine);
            health = 10;
        }
    }

    public void Rising(bool Out)
    {
        if (!raised)
        {
            PlayerMovement Body = player.GetComponent<PlayerMovement>();
            Body.body = gameObject;
            Body.attackRate = attackRate;
            Body.wich = wich;
            Body.attack = attackDamage;
            Body.canAttack = true;
            Body.range = range;

            PlayerShoot Shoot = player.GetComponent<PlayerShoot>();
            Shoot.BowPower = bowPower;
            Shoot.MaxBowCharge = maxBowCharge;
            Shoot.AttackMultiplier = arrowDamage;
            Shoot.MinimumRate = minimumRate;
            Shoot.wich = wich;

            PlayerHealth Health = player.GetComponent<PlayerHealth>();
            Health.body = gameObject;

            if ((wich == "Purple" || wich == "Dark"))
                bow.SetActive(true);
            else
                bow.SetActive(false);

            animator.SetTrigger("Wake");
            raised = true;

            healtBar.color = bar;
        }
        else
        {
            if (Out)
            {
                bow.SetActive(false);
                PlayerMovement Body = player.GetComponent<PlayerMovement>();
                Body.canAttack = false;
                Body.range = false;
            }
            animator.SetTrigger("Down");
            raised = false;
        }
    }
    IEnumerator Death(float checkDuration)
    {
        yield return new WaitForSeconds(checkDuration);
        Destroy(gameObject);
    }
}
