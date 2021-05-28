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

    public Button nextUpgradeButton;

    [TextArea]
    public string info;

    public void OnStart()
    {
        isPurchased = PlayerPrefs.GetInt(upgradeCategory);

        if (isPurchased == 0)
        {
            if(nextUpgradeButton != null)
            {
                nextUpgradeButton.interactable = false;
            }
        }
        else if(isPurchased == 1)
        {
            if (nextUpgradeButton != null)
            {
                nextUpgradeButton.interactable = true;
            }
        }
    }

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
            //levelButton.interactable = true;

            ColorBlock colors = levelButton.colors;

            colors.normalColor = Color.white;

            levelButton.colors = colors;
        }
    }

    public void PickUpgrade()
    {

    }

    public void OnUnlock()
    {
        if (nextUpgradeButton != null)
        {
            nextUpgradeButton.interactable = true;
        }
    }
    
}
