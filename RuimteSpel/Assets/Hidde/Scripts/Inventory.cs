using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Collider collectionCollider;

    public int greenCrystals;
    public int blueCrystals;

    public int inventoryAmount;

    public float maxSize;

    public TMP_Text greenCrystalText;

    private void Start()
    {
        maxSize = PlayerPrefs.GetFloat("StorageSpace");
    }

    private void Update()
    {
        greenCrystalText.text = "Green crystals: " + greenCrystals.ToString() + "/" + maxSize.ToString();

        if(inventoryAmount >= 0 && inventoryAmount < maxSize)
        {
            collectionCollider.isTrigger = false;
        }else if(inventoryAmount > 0 && inventoryAmount == maxSize)
        {
            collectionCollider.isTrigger = true;
        }
    }

    public void AddGreenCrystal(GameObject crystal)
    {
        if(inventoryAmount < maxSize)
        {
            Destroy(crystal);

            greenCrystals++;
            inventoryAmount++;

            PlayerPrefs.SetInt("GreenCrystals", greenCrystals);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "DefaultCrystal")
        {
            GetComponent<Inventory>().AddGreenCrystal(collision.gameObject);
        }
    }
}
