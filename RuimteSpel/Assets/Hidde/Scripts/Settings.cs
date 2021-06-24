using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public TMP_Dropdown resDropdown;

    Resolution[] resolutions;
    

    private void Start()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int resolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + "@" + resolutions[i].refreshRate;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                resolutionIndex = i;
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = resolutionIndex;
        resDropdown.RefreshShownValue();

    }

    public void SetResolution(int resolutionInd)
    {
        Resolution resolution = resolutions[resolutionInd];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }

    public void SetMasterVolume(float volume)
    {
        mixer.SetFloat("Master", volume);
    }

    public void SetFXVolume(float volume)
    {
        mixer.SetFloat("AFX", volume);
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("Music", volume);
    }

    public void SetQuality(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
    }

    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
