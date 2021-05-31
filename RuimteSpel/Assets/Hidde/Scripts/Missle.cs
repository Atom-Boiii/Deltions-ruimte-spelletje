using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public float speed;
    public float triggerDistance;

    public float damageAmount;

    public GameObject explosionEffect;

    private void Start()
    {
        StartCoroutine(DoMissle());
    }

    private void Update()
    {
        transform.position += transform.forward.normalized* speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, GameObject.Find("PlayerShip").transform.position) <= triggerDistance)
        {
            ExplodeMissle(true);
        }
    }

    public void ExplodeMissle(bool damage)
    {
        GameObject tempMissle = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(tempMissle, 5);

        if (damage)
        {
            FindObjectOfType<Health>().DoDamage(damageAmount);
        }

        Destroy(gameObject);
    }

    private IEnumerator DoMissle()
    {
        yield return new WaitForSeconds(10f);

        ExplodeMissle(false);
    }
}
