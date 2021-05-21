using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPurposeFighter : MonoBehaviour
{
    public Transform target;
    public Transform rotateOrigin;

    public float speed;
    public float weaponDamage;
    public float rotationSpeed;

    private bool canMove = true;


    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotateOrigin.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotateOrigin.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    public void SetMovementState(bool state)
    {
        canMove = state;
    }
}
