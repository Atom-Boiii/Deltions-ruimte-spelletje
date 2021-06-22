using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SpaceStationEntrance : MonoBehaviour
{
    public GameObject hangarCam;

    public PlayableDirector returnDirector;
    public bool isExterior;

    public void LeaveHangar()
    {
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ReturnToMenu()
    {
        hangarCam.SetActive(false);
        PlayerPrefs.SetInt("GameStarted", 0);
        returnDirector.Play();
    }
}
