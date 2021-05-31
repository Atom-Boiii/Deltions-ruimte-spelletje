using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button continueButton;

    private void Start()
    {
        if(PlayerPrefs.GetString("hasSave") == "true")
        {
            continueButton.interactable = true;
        }
    }

    public void NewGame()
    {
        ClearAll();
        PlayerPrefs.SetString("hasSave", "true");
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void ClearAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("MaxSpeed", 25f);
        PlayerPrefs.SetInt("GreenCrystals", 0);

        PlayerPrefs.SetFloat("MiningDamage", 20f);
        PlayerPrefs.SetFloat("MiningRate", 0.7f);
        PlayerPrefs.SetFloat("StorageSpace", 20);

        PlayerPrefs.SetInt("Thrust 1", 1);
        PlayerPrefs.SetInt("Mining 1", 1);
        PlayerPrefs.SetInt("Shooting 1", 1);
        PlayerPrefs.SetInt("Shield 1", 1);
        PlayerPrefs.SetInt("Evasion 1", 0);
        PlayerPrefs.SetInt("Power 1", 1);

        PlayerPrefs.SetFloat("ShootDistance", 100f);
        PlayerPrefs.SetFloat("ShootDamage", 10f);
        PlayerPrefs.SetFloat("ShootRate", 0.7f);

        PlayerPrefs.SetFloat("Shield", 25f);
        PlayerPrefs.SetFloat("Hull", 50f);
    }
}
