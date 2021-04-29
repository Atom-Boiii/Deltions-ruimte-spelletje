using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("Movement")]
    public bool movement;
    public float thrustSpeed, shootRange;
    public Transform gunOrigin, rotateOrigin;
    public Transform playerTarget;

    [Header("Health")]
    public float maxHealth;
    private float health, deathCooldown;
    public Rigidbody rb;
    public GameObject explosion, initialExplosion;

    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;

        OnStart();
    }

    public void Update()
    {
        if(playerTarget != null)
        {
            Debug.Log("sdsddsds");

            if (movement)
            {
                // If there is a target rotate towards the target and fly towards it
                rotateOrigin.transform.LookAt(playerTarget);
                direction = playerTarget.position - transform.position;
                transform.Translate(direction.normalized * thrustSpeed * Time.deltaTime, Space.World);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(9999f);
        }

        OnEndUpdate();
    }

    private void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if(health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        movement = false;

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }

    public virtual void OnStart() { }
    public virtual void OnEndUpdate() { }
}
