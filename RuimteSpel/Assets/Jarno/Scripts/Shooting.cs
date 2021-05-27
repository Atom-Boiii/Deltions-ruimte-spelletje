using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
        
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
            _Timer += 1 * Time.deltaTime;
             if (_Timer >= secondsBetweenShots)
               {
                    Shoot();
                    _Timer = 0;
               }
        }
        if (Input.GetMouseButton(1))
        {
            _Timer += 1 * Time.deltaTime;
            if (_Timer >= secondsBetweenMine)
            {
                Mine();
                _Timer = 0;
            }
            
        }
        else
        {
            lr.enabled = false;
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, shootPoint.TransformDirection(Vector3.forward), out hit, shootDistance, enemyLayer))
        {
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