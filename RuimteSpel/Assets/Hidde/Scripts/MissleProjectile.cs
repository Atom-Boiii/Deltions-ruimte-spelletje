using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleProjectile : MonoBehaviour
{
    public float speed;
    public float explosionDamage, explosionTimer, explosionRange, explosionTriggerRadius;

    private void Start()
    {
        StartCoroutine(Explode());
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionTimer);

        Destroy(gameObject);
    }
}
