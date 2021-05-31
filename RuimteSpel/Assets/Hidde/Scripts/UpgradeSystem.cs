using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    public Upgrade[] upgrades;

    public int cash = 0;

    private int pageIndex = 1;

    public TMP_Text pageIndexIndicator;
    public TMP_Text moneyCounter;
    public TMP_Text infoText;
    public TMP_Text costText;

    public GameObject upgradeMainPanel;
    public GameObject upgradePanel1;
    public GameObject upgradePanel2;

    private void Start()
    {
        Cursor.visible = true;

        cash = PlayerPrefs.GetInt("Money");

        upgradePanel1.SetActive(true);
        upgradePanel2.SetActive(false);

        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].OnStart();
            upgrades[i].isPurchased = PlayerPrefs.GetInt(upgrades[i].upgradeCategory);

            upgrades[i].UpdateUpgrade();
        }
    }

    private void Update()
    {
        cash = PlayerPrefs.GetInt("Money");

        Cursor.lockState = CursorLockMode.Confined;

        pageIndexIndicator.text = "(" + pageIndex + "/" + "2)";
        moneyCounter.text = "$" + cash;
    }

    public void OpenPanel()
    {
        upgradeMainPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        upgradeMainPanel.SetActive(false);
    }

    public void PageForward()
    {
        if(pageIndex == 1)
        {
            upgradePanel2.SetActive(true);
            upgradePanel1.SetActive(false);
            pageIndex++;
        }
    }

    public void PageBackward()
    {
        if (pageIndex == 2)
        {
            upgradePanel1.SetActive(true);
            upgradePanel2.SetActive(false);
            pageIndex--;

            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].UpdateUpgrade();
            }
        }
    }

    private int currentUpgrade = 0;
    
    public void PurchaseUpgrade()
    {
        if(cash >= upgrades[currentUpgrade].cost)
        {
            cash -= upgrades[currentUpgrade].cost;
            PlayerPrefs.SetInt("Money", cash);

            upgrades[currentUpgrade].isPurchased = 1;
            upgrades[currentUpgrade].UpdateUpgrade();
            upgrades[currentUpgrade].upgradeType.level = upgrades[currentUpgrade].level;
            upgrades[currentUpgrade].upgradeType.OnUpgrade();
            upgrades[currentUpgrade].OnUnlock();
            upgrades[currentUpgrade].isPurchased = 1;

            PlayerPrefs.SetInt(upgrades[currentUpgrade].upgradeCategory, upgrades[currentUpgrade].isPurchased);
        }
        else
        {
            Debug.Log("Not enough cash");
        }
    }

    public void SetUpgrade(int upgradeIndex)
    {
        currentUpgrade = upgradeIndex;

        infoText.text = upgrades[upgradeIndex].info;
        costText.text = "Price: $" + upgrades[upgradeIndex].cost.ToString();
        
    }
}
