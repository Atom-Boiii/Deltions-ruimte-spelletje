using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    public GameObject[] crystals;

    public Vector2 scaleRange;
    public float maxHealth;
    public GameObject explosionEffect;

    private float health;

    private void Start()
    {
        float size = Random.Range(scaleRange.x, scaleRange.y);
        transform.localScale = new Vector3(size, size, size);

        health = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(999f);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        
        if(health <= 0f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        GameObject go = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(go, 5f);
        Destroy(gameObject);

        int random = Random.Range(2, 5);

        for (int i = 0; i < random; i++)
        {
            GameObject tempCrystal = crystals[Random.Range(0, crystals.Length)];
            Instantiate(tempCrystal, transform.position, transform.rotation);
        }
    }
}
