using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shooting Upgrade", menuName = "Upgrades/Shooting")]
public class ShootingItem : UpgradeItem
{
    public override void OnUpgrade()
    {
        base.OnUpgrade();

        if (level == 0)
        {
            PlayerPrefs.SetFloat("ShootDistance", 100f);
            PlayerPrefs.SetFloat("ShootDamage", 10f);
            PlayerPrefs.SetFloat("ShootRate", 1f);
        }
        else if (level == 1)
        {
            PlayerPrefs.SetFloat("ShootDistance", 150f);
            PlayerPrefs.SetFloat("ShootDamage", 20f);
            PlayerPrefs.SetFloat("ShootRate", 2f);
        }
        else if (level == 2)
        {
            PlayerPrefs.SetFloat("ShootDistance", 200f);
            PlayerPrefs.SetFloat("ShootDamage", 30f);
            PlayerPrefs.SetFloat("ShootRate", 3f);
            PlayerPrefs.SetString("HasSecondShooting", "true");
        }
        else if (level == 3)
        {
            PlayerPrefs.SetFloat("ShootDistance", 250f);
            PlayerPrefs.SetFloat("ShootRate", 4f);
        }
    }
}
