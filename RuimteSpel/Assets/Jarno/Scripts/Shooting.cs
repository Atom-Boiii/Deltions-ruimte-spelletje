using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
        
   public float damage = 20;
   public float shootDistance = 50;
   public Transform shootpoint;
   public LayerMask enemyLayer;

    public LineRenderer lr;

    public LayerMask mineralLayer;
    public float mineDistance = 10f;
   

   
   public float _SecondsBetweenShots = 0.5f;


    private float _Timer;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _Timer += 1 * Time.deltaTime;
             if (_Timer >= _SecondsBetweenShots)
               {
                    Shoot();
                    _Timer = 0;
               }
        }
        if (Input.GetMouseButton(1))
        {
            Mine();
        }
        else
        {
            lr.enabled = false;
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, shootpoint.TransformDirection(Vector3.forward), out hit, shootDistance, enemyLayer))
        {
            hit.transform.GetComponent<EnemyShip>().TakeDamage(damage);
        }
                
    }
    void Mine()
    {
        RaycastHit hit;
        bool checkhit = false;
        if (Physics.Raycast(transform.position, shootpoint.TransformDirection(Vector3.forward), out hit, mineDistance * 1000, mineralLayer))
        {
            if (hit.transform.GetComponent<Astroid>() != null)
            {
                hit.transform.GetComponent<Astroid>().TakeDamage(damage);
            }
            lr.enabled = true;
            checkhit = true;
            lr.SetPositions(new Vector3[] { shootpoint.position, hit.point });
        }
        
        if(!checkhit)
        {
            lr.enabled = false;
        }
    }
}