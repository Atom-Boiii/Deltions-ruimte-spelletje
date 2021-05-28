using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evasion : UpgradeItem
{
    public override void OnUpgrade()
    {
        base.OnUpgrade();

        if (level == 0)
        {
            PlayerPrefs.SetInt("Evasion 1", 1);
        }
        else if (level == 1)
        {
            PlayerPrefs.SetInt("Evasion 1", 2);
        }
        else if (level == 2)
        {
            PlayerPrefs.SetInt("Evasion 1", 3);
        }
        else if (level == 3)
        {
            PlayerPrefs.SetInt("Evasion 1", 4);
        }
    }
}
