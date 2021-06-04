using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public bool rechargeShield;
    public float shieldRechargeRate;

    private bool isBeingHit;

    [Header("Health")]
    public float maxHealth;
    public float maxShield;
    private float health, shield, deathCooldown;
    public Rigidbody rb;
    public GameObject explosion, initialExplosion;
    public float damageMultiplier, shieldDamageMultiplier;

    private Vector3 direction;

    private float rechargeTimer;

    private bool hitBool;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        shield = maxShield;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(9999f);
        }

        if (rechargeShield)
        {
            if (!isBeingHit)
            {
                if (Time.time >= rechargeTimer)
                {
                    rechargeTimer = Time.time + 1f / shieldRechargeRate;

                    if (shield <= maxShield - 1)
                    {
                        hitBool = false;
                        shield++;
                    }
                }
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if(shield <= 0)
        {
            health -= damageAmount * damageMultiplier;
        }else if(shield > 0)
        {
            shield -= damageAmount * shieldDamageMultiplier;
        }

        if(health <= 0f)
        {
            Die();
        }

        if (!hitBool)
        {
            StartCoroutine(SetHitStatus());
        }

        isBeingHit = true;
    }

    private void Die()
    {
        rb.isKinematic = false;
        rb.AddForce(transform.forward * 1000f);
        rb.AddTorque(new Vector3(50f, 50f, 50f));

        GameObject go = Instantiate(initialExplosion, transform.position, transform.rotation);

        Destroy(go, 2f);
        StartCoroutine(DestroyShip());
    }

    private IEnumerator DestroyShip()
    {
        deathCooldown = Random.Range(1f, 3f);

        yield return new WaitForSeconds(deathCooldown);

        Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);

        GameObject go = Instantiate(explosion, transform.position, rotation);

        Destroy(go, 10f);
        Destroy(gameObject);
    }

    private IEnumerator SetHitStatus()
    {
        hitBool = true;

        yield return new WaitForSeconds(1f);

        isBeingHit = false;
    }
}
