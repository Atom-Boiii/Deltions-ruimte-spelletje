using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorUpgraded : MonoBehaviour
{
    public ParticleSystem[] shootEffect;

    public Transform indicator;

    public float torque = 5f;
    public float thrust = 10f;
    public float rotationSpeed = 2f;
    private Rigidbody rb;
    public Transform target;

    public float hitRange;
    public LayerMask mask;
    public float fireRate;
    public float damage;

    public Vector3 offset;

    private float nextTimeToFire;

    private void Start()
    {
        target = GameObject.Find("PlayerShip").transform;

        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (indicator != null)
        {
            indicator.LookAt(target);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, hitRange, mask))
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(hit);
            }
        }

        Vector3 lookLocation = target.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(lookLocation);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    private void FixedUpdate()
    {
        Vector3 targetLocation = target.position - transform.position + offset;

        float distance = targetLocation.magnitude;

        Vector3 translation = Vector3.forward * Mathf.Clamp((distance - 10f) / 50f, 0f, 1f) * thrust;

        rb.AddRelativeForce(translation * Time.fixedDeltaTime);
    }

    private void Shoot(RaycastHit data)
    {
        for (int i = 0; i < shootEffect.Length; i++)
        {
            shootEffect[i].Play();
        }
        if (data.transform.tag == "Player")
        {
            data.transform.GetComponent<Health>().DoDamage(damage);
        }
    }
}
