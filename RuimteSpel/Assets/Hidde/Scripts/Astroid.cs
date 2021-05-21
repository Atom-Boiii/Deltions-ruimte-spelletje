using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    public GameObject[] crystals;

    public Vector2 scaleRange;
    public float maxHealth;
    public GameObject explosionEffect;
    public int crystalAmount;

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

    Vector3 crystalPos;

    private void Explode()
    {
        FindObjectOfType<AstroidSpawner>().SpawnAstroid();

        GameObject go = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(go, 5f);
        Destroy(gameObject);

        for (int i = 0; i < crystalAmount; i++)
        {
            int random2 = Random.Range(-5, 5);

            crystalPos = transform.position + new Vector3(random2, random2, random2);

            GameObject tempCrystal = crystals[Random.Range(0, crystals.Length)];
            Instantiate(tempCrystal, crystalPos, transform.rotation);
        }
    }
}
