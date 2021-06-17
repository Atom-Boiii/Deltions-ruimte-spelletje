using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MiningUpgrade", menuName = "Upgrades/Mining")]
public class Mining : UpgradeItem
{
    public override void OnUpgrade()
    {
        base.OnUpgrade();

        if (level == 0)
        {
            PlayerPrefs.SetFloat("MiningDamage", 20f);
            PlayerPrefs.SetFloat("MiningRate", 1f);
            PlayerPrefs.SetFloat("StorageSpace", 20);
        }
        else if (level == 1)
        {
            PlayerPrefs.SetFloat("MiningRate", 2f);
            PlayerPrefs.SetFloat("MiningDamage", 25f);
            PlayerPrefs.SetFloat("StorageSpace", 30);
        }
        else if (level == 2)
        {
            PlayerPrefs.SetFloat("StorageSpace", 60);
            PlayerPrefs.SetString("HasSecondMining", "true");
        }
        else if (level == 3)
        {
            PlayerPrefs.SetFloat("MiningRate", 3f);
            PlayerPrefs.SetFloat("MiningDamage", 40f);
        }
    }
}
