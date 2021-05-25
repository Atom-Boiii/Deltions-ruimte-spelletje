using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyFighter : MonoBehaviour
{
    public Transform target, rotateOrigin;
    public Vector3 trackOffset;
    public GameObject missle;

    public Transform[] spawnPoints;

    public float speed, rotationSpeed;

    public float missleDistance, evadeDistance, maxEvadeDistance;

    int attackStage;

    private Vector3 dir;
    Quaternion lookRotation;
    Vector3 rotation;
    Vector3 flyDirection;

    private bool canShootMissle = true;


    private void Update()
    {
        if(Vector3.Distance(transform.position, target.position) >= missleDistance)
        {
            ShootMissle();
        }

        if(Vector3.Distance(transform.position, target.position) <= evadeDistance)
        {
            attackStage = 1;
        }else if(Vector3.Distance(transform.position, target.position) >= maxEvadeDistance)
        {
            attackStage = 0;
        }

        switch (attackStage)
        {
            default:
                dir = target.position - transform.position;
                lookRotation = Quaternion.LookRotation(dir);
                rotation = Quaternion.Lerp(rotateOrigin.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
                rotateOrigin.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

                flyDirection = dir += trackOffset;

                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
                break;
            case 1:
                dir = -target.forward * 100f;

                lookRotation = Quaternion.LookRotation(dir);
                rotation = Quaternion.Lerp(rotateOrigin.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
                rotateOrigin.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

                flyDirection = dir += trackOffset;

                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
                break;
        }
    }

    private void ShootMissle()
    {
        if (canShootMissle)
        {
            canShootMissle = false;
            int missleIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(missle, spawnPoints[missleIndex].position, Quaternion.LookRotation(rotateOrigin.forward));

            StartCoroutine(SetShootState());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, missleDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, evadeDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxEvadeDistance);
    }

    private IEnumerator SetShootState()
    {
        yield return new WaitForSeconds(5f);

        canShootMissle = true;
    }
}
