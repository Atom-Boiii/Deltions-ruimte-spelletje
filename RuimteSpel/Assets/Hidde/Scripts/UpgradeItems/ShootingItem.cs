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
            PlayerPrefs.SetFloat("ShootDistance", 25f);
            PlayerPrefs.SetFloat("ShootDamage", 10f);
            PlayerPrefs.SetFloat("ShootRate", 0.7f);
        }
        else if (level == 1)
        {
            PlayerPrefs.SetFloat("ShootDistance", 50f);
            PlayerPrefs.SetFloat("ShootDamage", 20f);
            PlayerPrefs.SetFloat("ShootRate", 0.55f);
        }
        else if (level == 2)
        {
            PlayerPrefs.SetFloat("ShootDistance", 75f);
            PlayerPrefs.SetFloat("ShootDamage", 30f);
            PlayerPrefs.SetFloat("ShootRate", 0.35f);
        }
        else if (level == 3)
        {
            PlayerPrefs.SetFloat("ShootDistance", 100f);
            PlayerPrefs.SetFloat("ShootRate", 0.2f);
        }
    }
}
