using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public ParticleSystem[] shootEffects;
        
   public float damage = 20;
    public float miningDamage = 20;

   public float shootDistance = 50;
    public Transform shootPoint;
    public Transform spherePoint;
   public LayerMask enemyLayer;

    public LineRenderer lr;

    public LayerMask mineralLayer;
    public float mineDistance = 10f;
   

   
    public float secondsBetweenShots = 0.5f;
    public float secondsBetweenMine = 0.5f;


    private float _Timer;
    private float nextTimeToFire;

    private void Start()
    {
        secondsBetweenMine = PlayerPrefs.GetFloat("MiningRate");
        miningDamage = PlayerPrefs.GetFloat("MiningDamage");

        damage = PlayerPrefs.GetFloat("ShootDamage");
        secondsBetweenShots = PlayerPrefs.GetFloat("ShootRate");
        shootDistance = PlayerPrefs.GetFloat("ShootDistance");
    }

    void Update()
    {
        lr.SetPosition(0, spherePoint.position);

        if (Input.GetMouseButton(0))
        {
             if (Time.time >= nextTimeToFire)
             {
                nextTimeToFire = Time.time + 1f / secondsBetweenShots;
                Shoot();
             }
        }
        if (Input.GetMouseButton(1))
        {
            RaycastHit _hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out _hit, mineDistance * 1000, mineralLayer))
            {
                lr.SetPosition(1, _hit.point);
            }
            else
            {
                lr.SetPosition(1, spherePoint.position);
            }

            if (Time.time >= _Timer)
            {
                _Timer = Time.time + 1f/ secondsBetweenMine;
                Mine();
            }
            
        }
        else
        {
            lr.SetPosition(1, spherePoint.position);
        }
    }
    void Shoot()
    {
        for (int i = 0; i < shootEffects.Length; i++)
        {
            shootEffects[i].Play();
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, shootPoint.forward, out hit, shootDistance * 10, enemyLayer))
        {
            Debug.Log("I hit " + hit.transform.name);
            hit.transform.GetComponent<EnemyShip>().TakeDamage(damage);
        }
                
    }
    void Mine()
    {
        RaycastHit hit;
        bool checkhit = false;
        if (Physics.Raycast(transform.position, shootPoint.forward, out hit, mineDistance * 1000, mineralLayer))
        {
            if (hit.transform.GetComponent<Astroid>() != null)
            {
                hit.transform.GetComponent<Astroid>().TakeDamage(miningDamage);
            }
            lr.enabled = true;
            checkhit = true;
            lr.SetPosition(1, hit.point);
        }
        
        if(!checkhit)
        {
            lr.enabled = false;
        }
    }
}