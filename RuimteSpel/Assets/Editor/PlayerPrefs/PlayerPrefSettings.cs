using UnityEditor;
using UnityEngine;

public class PlayerPrefSettings : MonoBehaviour
{
    [MenuItem("Saves/ResetSaves")]
    static void DoSomething()
    {
        PlayerPrefs.DeleteAll();
    }
}
