using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Thrust Upgrade", menuName = "Upgrades/Thrust")]
public class Thrust : UpgradeItem
{
    public override void OnUpgrade()
    {
        base.OnUpgrade();

        if(level == 0)
        {
            PlayerPrefs.SetFloat("MaxSpeed", 25f);
        }else if (level == 1)
        {
            PlayerPrefs.SetFloat("MaxSpeed", 50f);
        }
        else if (level == 2)
        {
            PlayerPrefs.SetFloat("MaxSpeed", 75f);
        }
        else if (level == 3)
        {
            PlayerPrefs.SetFloat("MaxSpeed", 100f);
        }
    }
}
