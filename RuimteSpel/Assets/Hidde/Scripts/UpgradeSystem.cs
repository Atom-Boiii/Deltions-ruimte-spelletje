using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    public int cash = 0;

    private int pageIndex = 1;

    public TMP_Text pageIndexIndicator;
    public TMP_Text moneyCounter;

    public GameObject upgradeMainPanel;
    public GameObject upgradePanel1;
    public GameObject upgradePanel2;

    private void Start()
    {
        //cash = PlayerPrefs.GetInt("Money");

        upgradePanel1.SetActive(true);
        upgradePanel2.SetActive(false);
    }

    private void Update()
    {
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
        }
    }
}
