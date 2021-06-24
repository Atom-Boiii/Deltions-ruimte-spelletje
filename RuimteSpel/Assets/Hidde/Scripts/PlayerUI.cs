using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public void ReturnToStation()
    {
        PlayerPrefs.SetInt("GameStarted", 1);
        SceneManager.LoadScene(0);
    }

    public void ReturnToMenu()
    {
        PlayerPrefs.SetInt("GameStarted", 0);
        SceneManager.LoadScene(0);
    }
}
