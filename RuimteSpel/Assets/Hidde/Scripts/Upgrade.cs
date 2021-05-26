using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Upgrade 
{
    public string upgradeCategory;
    public Button levelButton;
    public int isPurchased = 0;
    public int cost, level;
    public UpgradeItem upgradeType;

    [TextArea]
    public string info;

    public void UpdateUpgrade()
    {
        if (isPurchased == 1)
        {
            levelButton.interactable = false;
            ColorBlock colors = levelButton.colors;

            colors.disabledColor = Color.green;

            levelButton.colors = colors;  
        }
        else if(isPurchased == 0)
        {
            levelButton.interactable = true;

            ColorBlock colors = levelButton.colors;

            colors.normalColor = Color.white;

            levelButton.colors = colors;
        }
    }

    public void PickUpgrade()
    {

    }
    
}
