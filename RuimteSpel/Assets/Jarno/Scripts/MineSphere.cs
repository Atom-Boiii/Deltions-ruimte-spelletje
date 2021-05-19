using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSphere : MonoBehaviour
{

    public GameObject player;
    public float orbitSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitAround();
    }

    void OrbitAround()
    {
        player.transform.Rotate(0,0,orbitSpeed  * Time.deltaTime);
    }
}
