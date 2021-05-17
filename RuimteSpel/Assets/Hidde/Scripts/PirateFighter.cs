using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateFighter : EnemyShip
{
    public LayerMask mask;

    public float range = 100f;

    private float nextTimeToFire = 0f;

    [Header("Shooting")]
    public float fireRate;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnEndUpdate()
    {
        base.OnEndUpdate();

        if(playerTarget != null)
        {
            if (Vector3.Distance(transform.position, playerTarget.position) < shootRange)
            {
                if (Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
                }
            }
        }
    }

    private void Shoot()
    {
        RaycastHit _hit;

        gunOrigin.Rotate(0f, 0f, rotateSpeed);

        if (Physics.Raycast(rotateOrigin.position, rotateOrigin.forward, out _hit, range, mask))
        {
            Debug.Log("I hit" + _hit.transform.name);
        }
    }
}
