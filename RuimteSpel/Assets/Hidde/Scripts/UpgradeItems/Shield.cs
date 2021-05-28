using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : UpgradeItem
{
    public override void OnUpgrade()
    {
        base.OnUpgrade();

        if (level == 0)
        {

            PlayerPrefs.SetFloat("Shield", 25f);
            PlayerPrefs.SetFloat("Hull", 50f);
        }
        else if (level == 1)
        {
            PlayerPrefs.SetFloat("Shield", 50f);
            PlayerPrefs.SetFloat("Hull", 100f);
        }
        else if (level == 2)
        {
            PlayerPrefs.SetFloat("Shield", 75f);
        }
        else if (level == 3)
        {
            PlayerPrefs.SetFloat("Shield", 100f);
            PlayerPrefs.SetFloat("Hull", 150f);
        }
    }
}
