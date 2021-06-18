using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
    public GameObject astroidPrefab;
    public GameObject[] satellitePrefabs;

    public float startAmount;
    public float spawnRange;

    public float astroidSpawnChance;
    public float satelliteSpawnChance;

    private void Start()
    {
        for (int i = 0; i < startAmount; i++)
        {
            float random = Random.Range(0, 100f);

            if(random > astroidSpawnChance)
            {
                SpawnAstroid();
            }else if(random <= satelliteSpawnChance)
            {
                SpawnSatellite();
            }
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

    public void SpawnSatellite()
    {
        int rando = Random.Range(0, satellitePrefabs.Length);

        Vector3 position = transform.position + Random.insideUnitSphere * spawnRange;

        float x = Random.Range(0, 360);
        float y = Random.Range(0, 360);
        float z = Random.Range(0, 360);

        Instantiate(satellitePrefabs[rando], position, Quaternion.Euler(x, y, z));
    }
}
