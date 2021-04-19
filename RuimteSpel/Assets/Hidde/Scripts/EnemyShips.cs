using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShips : MonoBehaviour
{
    public bool movement;
    public float thrustSpeed, shootRange, speed;
    public Transform gunOrigin, rotateOrigin;

    public Transform target;

    private Vector3 direction;

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
                float step = speed * Time.deltaTime;

                // If there is a target rotate towards the target and fly towards it
                rotateOrigin.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
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
