using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    public void OnSpawn(float scaleX, float scaleY, float scaleZ)
    {
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    public void OnSpawn(Vector3 size)
    {
        transform.localScale = size;
    }
}
