using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public ObjectPool_Pool[] ObjectPools = null;

    private List <Transform> Parents = new List<Transform>();

    private void Awake()
    {
        GameObject emptyobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(emptyobject.GetComponent<MeshRenderer>());
        Destroy(emptyobject.GetComponent<BoxCollider>());

        for (int i = 0; i < ObjectPools.Length; i++)
        {
            //Create parent
            GameObject poolparent = Instantiate(emptyobject, transform.position, Quaternion.identity);
            Destroy(poolparent.GetComponent<MeshRenderer>());
            Destroy(poolparent.GetComponent<BoxCollider>());

            //Set parent
            poolparent.transform.parent = transform;
            poolparent.transform.name = "Pool_" + ObjectPools[i].Name;
            Parents.Add(poolparent.transform);

            //Create objects
            for (int o = 0; o < ObjectPools[i].Amount; o++)
            {
                GameObject obj = (GameObject)Instantiate(ObjectPools[i].Prefab);
                obj.transform.parent = poolparent.transform;
                obj.transform.position = new Vector2(9999, 9999);
                obj.SetActive(false);
                ObjectPools[i].Objects.Add(obj);
            }
        }
        Destroy(emptyobject);
    }

    //GetObject
    public GameObject GetObject(string objname, bool setactive)
    {
        int id = FindObjectPoolID(objname, false);
        return GetObject(id, setactive);
    }
    public GameObject GetObject(GameObject obj, bool setactive)
    {
        int id = FindObjectPoolID(obj);
        return GetObject(id, setactive);
    }
    public GameObject GetObjectPrefabName(string prefabname, bool setactive)
    {
        int id = FindObjectPoolID(prefabname, true);
        return GetObject(id, setactive);
    }

    public GameObject GetObject(int id, bool setactive)
    {
        GameObject freeObject = null;
        bool checkfreeobj = false;

        for (int i = 0; i < ObjectPools[id].Objects.Count; i++)
        {
            if (!ObjectPools[id].Objects[i].activeInHierarchy)
            {
                ObjectPools[id].Objects[i].transform.position = new Vector3(999, 999, 999);
                ObjectPools[id].Objects[i].SetActive(setactive);
                freeObject = ObjectPools[id].Objects[i];
                return freeObject;
            }
        }

        if (!checkfreeobj)
        {
            ObjectPools[id].Objects.Clear();
            freeObject = (GameObject)Instantiate(ObjectPools[id].Prefab, new Vector3(999, 999, 999), Quaternion.identity);
            freeObject.transform.parent = Parents[id];
            freeObject.SetActive(setactive);
            ObjectPools[id].Objects.Add(freeObject);
            return freeObject;
        }

        Debug.Log("No Object Found");
        return null;
    }

    public List<GameObject> GetAllObjects(GameObject objtype)
    {
        int id = FindObjectPoolID(objtype);
        return ObjectPools[id].Objects;
    }

    private int FindObjectPoolID(GameObject obj)
    {
        int id = 0;
        for (int i = 0; i < ObjectPools.Length; i++)
        {
            if (obj == ObjectPools[i].Prefab)
            {
                id = i;
            }
        }
        return id;
    }
    private int FindObjectPoolID(string objname, bool isprefab)
    {
        for (int i = 0; i < ObjectPools.Length; i++)
        {
            if (isprefab)
                if (objname == ObjectPools[i].Prefab.name)
                {
                    return i;
                }
                else
            if (objname == ObjectPools[i].Name)
                {
                    return i;
                }
        }
        Debug.Log(objname + " Not Found");
        return 0;
    }
}

[System.Serializable]
public class ObjectPool_Pool
{
    public string Name;
    public GameObject Prefab;
    public int Amount;
    public List<GameObject> Objects;
}