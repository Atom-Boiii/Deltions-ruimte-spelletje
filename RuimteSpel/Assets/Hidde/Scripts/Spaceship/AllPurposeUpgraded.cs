﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPurposeUpgraded : MonoBehaviour
{
    public ParticleSystem shootEffect;
    public Transform indicator;
    public TextMesh indicatorText;

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
        rb = GetComponent<Rigidbody>();
    }

    /*
    private void Bal()
    {
        //dir = target.position - transform.position;
        lookRotation = Quaternion.LookRotation(dir);
        rotation = Quaternion.Lerp(rotateOrigin.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotateOrigin.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
    */

    private void Update()
    {
        indicator.LookAt(target);

        float distance = Vector3.Distance(transform.position, target.position);
        indicatorText.fontSize = (int)distance;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, hitRange, mask))
        {
            if(Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(hit);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetLocation = target.position - transform.position + offset;
        Vector3 lookLocation = target.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(lookLocation);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

        float distance = targetLocation.magnitude;

        Vector3 translation = Vector3.forward * Mathf.Clamp((distance - 10f) / 50f, 0f, 1f) * thrust;

        rb.AddRelativeForce(translation* Time.fixedDeltaTime);
    }

    private void Shoot(RaycastHit data)
    {
        shootEffect.Play();
        if(data.transform.tag == "Player")
        {
            data.transform.GetComponent<Health>().DoDamage(damage);
        }
    }
}
