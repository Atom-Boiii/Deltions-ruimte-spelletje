using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;

public class MainMenu : MonoBehaviour
{
    public PlayableDirector playDirector;
    public PlayableDirector settingsDirector1;
    public PlayableDirector settingsDirector2;
    public PlayableDirector quitDirector1;
    public PlayableDirector quitDirector2;

    public void NewGame()
    {
        // Clear the playerPrefs
        // Run the playDirector

        ClearAllPrefs();

        FindObjectOfType<SceneController>().CallStart();
        playDirector.Play();
    }

    public void LoadGame()
    {
        // Run the playDirector

        FindObjectOfType<SceneController>().CallStart();
        playDirector.Play();
    }

    public void OpenSettings()
    {
        // Open the settings screen

        settingsDirector1.Play();
    }

    public void CloseSettings()
    {
        // Close the settings

        settingsDirector2.Play();
    }

    public void CallQuit()
    {
        quitDirector1.Play();
    }

    public void CancelQuit()
    {
        quitDirector2.Play();
    }

    public void QuitGame()
    {
        // Open quitting screen

        EditorApplication.isPlaying = false;
    }

    private void ClearAllPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("hasSave", "true");

        PlayerPrefs.SetFloat("MaxSpeed", 25f);

        PlayerPrefs.SetFloat("MiningDamage", 20f);
        PlayerPrefs.SetFloat("MiningRate", 1f);
        PlayerPrefs.SetFloat("StorageSpace", 20);

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
