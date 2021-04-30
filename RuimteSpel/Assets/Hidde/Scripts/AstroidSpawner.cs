using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
    public GameObject astroid;
    public float amount;
    public float radius;
    public float distance;

    private Vector3 lastPos;

    private List<GameObject> astroids = new List<GameObject>();

    private void Start()
    {
        lastPos = transform.position;
        SpawnAstroid("Out");
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, lastPos) > distance)
        {
            SpawnAstroid("In");
        }
    }

    private void SpawnAstroid(string scaling)
    {
        for (int i = 0; i < astroids.Count; i++)
        {
            Destroy(astroids[i], 60f);
        }

        lastPos = transform.position;

        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnLocations = Vector3.zero;

            if(scaling == "In")
            {
                spawnLocations = Random.insideUnitSphere * radius;

            }else if(scaling == "Out")
            {
                spawnLocations = Random.onUnitSphere * radius;
            }

            GameObject tempAst = Instantiate(astroid, transform.position + spawnLocations, Random.rotation);

            astroids.Add(tempAst);
        }
    }
}
