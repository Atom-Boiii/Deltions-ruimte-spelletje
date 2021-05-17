using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
        [Header("Settings")]
    [SerializeField] private float _Damage = 20;
    [SerializeField] private float _ShootDistance = 50;
    [SerializeField] private Transform _Shootpoint;
    [SerializeField] private LayerMask _EnemyLayer;

    [Header("Automatic")]
    [SerializeField] private float _SecondsBetweenShots = 0.5f;


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
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, _Shootpoint.TransformDirection(Vector3.forward), out hit, _ShootDistance, _EnemyLayer))
                hit.transform.GetComponent<Health>().DoDamage(_Damage);
    }
}