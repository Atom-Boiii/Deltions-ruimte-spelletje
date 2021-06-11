using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StationInventory : MonoBehaviour
{
    public GameObject marketPanel;

    public TMP_Text crystalText;

    public int greenCrystalValue;

    private int greenCrystals;

    public void Initiate()
    {
        greenCrystals = PlayerPrefs.GetInt("GreenCrystals");
        crystalText.text = "Green crystals: " + greenCrystals;
    }

    public void SellGreenCrystal()
    {
        int tempCash = PlayerPrefs.GetInt("Money");

        for (int i = 0; i < greenCrystals; i++)
        {
            tempCash += greenCrystalValue;
        }

        PlayerPrefs.SetInt("Money", tempCash);
        PlayerPrefs.SetInt("GreenCrystals", 0);

        greenCrystals = 0;

        crystalText.text = "Green crystals: " + greenCrystals;
    }

    public void OpenMarketPanel()
    {
        marketPanel.SetActive(true);
    }

    public void CloseMarketPanel()
    {
        marketPanel.SetActive(false);
    }
}
