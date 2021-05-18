using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
    public GameObject astroidPrefab;

    public float startAmount;
    public float spawnRange;

    private void Start()
    {
        for (int i = 0; i < startAmount; i++)
        {
            SpawnAstroid();
        }
    }

    public void SpawnAstroid()
    {
        Vector3 position = transform.position + Random.insideUnitSphere * spawnRange;

        float x = Random.Range(0, 360);
        float y = Random.Range(0, 360);
        float z = Random.Range(0, 360);

        Instantiate(astroidPrefab, position, Quaternion.Euler(x, y, z));
    }
}
