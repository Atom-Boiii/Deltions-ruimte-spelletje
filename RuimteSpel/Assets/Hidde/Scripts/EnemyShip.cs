using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public bool movement;
    public float thrustSpeed, shootRange;
    public Transform gunOrigin, rotateOrigin;
    public Transform target;

    public float maxHealth;
    private float health;


    private Vector3 direction;

    private void Start()
    {
        
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

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
