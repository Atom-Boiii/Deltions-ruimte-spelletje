using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("Movement")]
    public bool movement;
    public float thrustSpeed, shootRange;
    public Transform gunOrigin, rotateOrigin;
    public Transform target;

    [Header("Health")]
    public float maxHealth;
    private float health;
    public Rigidbody rb;
    public GameObject explosion, initialExplosion;

    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
    }

    public void Update()
    {
        if (movement)
        {
            if (target == null)
            {
                // If there is no target just go forward
                direction = transform.forward * thrustSpeed;
                transform.Translate(direction.normalized * thrustSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                // If there is a target rotate towards the target and fly towards it
                rotateOrigin.transform.LookAt(target);
                direction = target.position - transform.position;
                transform.Translate(direction.normalized * thrustSpeed * Time.deltaTime, Space.World);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(9999f);
        }

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

        GameObject go = Instantiate(initialExplosion, transform.position, transform.rotation);

        Destroy(go, 2f);
        StartCoroutine(DestroyShip());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }

    private IEnumerator DestroyShip()
    {
        Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
        yield return new WaitForSeconds(3f);
        GameObject go = Instantiate(explosion, transform.position, rotation);
        Destroy(go, 10f);
        Destroy(gameObject);
    }
}
