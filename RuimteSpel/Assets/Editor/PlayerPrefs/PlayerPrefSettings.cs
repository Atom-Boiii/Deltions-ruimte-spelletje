using UnityEditor;
using UnityEngine;

public class PlayerPrefSettings : MonoBehaviour
{
    [MenuItem("Saves/ResetSaves")]
    static void DoSomething()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("MaxSpeed", 25f);
        PlayerPrefs.SetFloat("MiningDamage", 20f);
        PlayerPrefs.SetFloat("MiningRate", 0.7f);
        PlayerPrefs.SetFloat("StorageSpace", 20);
    }
}
