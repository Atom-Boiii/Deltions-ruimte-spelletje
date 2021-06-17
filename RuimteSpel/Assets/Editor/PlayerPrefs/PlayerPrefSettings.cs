using UnityEditor;
using UnityEngine;

public class PlayerPrefSettings : MonoBehaviour
{
    [MenuItem("Saves/ResetSaves")]
    static void DoSomething()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("hasSave", "true");

        PlayerPrefs.SetFloat("MaxSpeed", 25f);

        PlayerPrefs.SetFloat("MiningDamage", 20f);
        PlayerPrefs.SetFloat("MiningRate", 1f);
        PlayerPrefs.SetFloat("StorageSpace", 20);

        PlayerPrefs.SetString("HasSecondMining", "false");
        PlayerPrefs.SetString("HasSecondShooting", "false");

        PlayerPrefs.SetInt("GameStarted", 0);

        PlayerPrefs.SetInt("Thrust 1", 1);
        PlayerPrefs.SetInt("Mining 1", 1);
        PlayerPrefs.SetInt("Shooting 1", 1);
        PlayerPrefs.SetInt("Shield 1", 1);
        PlayerPrefs.SetInt("Evasion 1", 0);
        PlayerPrefs.SetInt("Power 1", 1);

        PlayerPrefs.SetFloat("ShootDistance", 100f);
        PlayerPrefs.SetFloat("ShootDamage", 10f);
        PlayerPrefs.SetFloat("ShootRate", 2f);

        PlayerPrefs.SetFloat("Shield", 50f);
        PlayerPrefs.SetFloat("Hull", 75f);
    }
}
