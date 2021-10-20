using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] Animator animator;
    [SerializeField] Transform Bow;
    [Range(0, 15)]
    public float BowPower;
    [Range(0, 3)]
    public float MaxBowCharge;
    public float AttackMultiplier = 5f;
    public float MinimumRate = 0.25f;
    public string wich = "none";

    private float bowCharge;

    private bool canFire = true;

    private bool dark = false;

    private void Update()
    {
        if (dark)
        {
            animator.SetFloat("BowCharge", bowCharge);

            if (Input.GetMouseButton(0) && canFire)
                ChargeBow();
            else if (Input.GetMouseButtonUp(0) && canFire)
                FireBow();
            else
            {
                if (bowCharge > 0f)
                    bowCharge -= 1f * Time.deltaTime;
                else
                {
                    bowCharge = 0f;
                    canFire = true;
                }
            }
        }
    }

    public void Darker(bool dork)
    {
        dark = dork;
    }

    private void ChargeBow()
    {
        bowCharge +=  Time.deltaTime;
    }

    private void FireBow()
    {
        if (bowCharge > MinimumRate)
        {
            if (bowCharge > MaxBowCharge)
                bowCharge = MaxBowCharge;

            float arrowSpeed = bowCharge + BowPower;

            float angle = Utility.AngleTowardsMouse(Bow.position);
            Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

            Arrow Arrow = Instantiate(ArrowPrefab, Bow.position, rot).GetComponent<Arrow>();
            Arrow.ArrowVelocity = arrowSpeed;
            Arrow.wich = wich;
            Arrow.ArrowDamage = AttackMultiplier * bowCharge;

            canFire = false;
        }
        bowCharge = 0f;
    }
}
