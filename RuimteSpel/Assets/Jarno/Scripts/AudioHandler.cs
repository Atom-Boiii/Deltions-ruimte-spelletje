using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour
{
    
    public bool refreshSettingsOnUpdate = false;
    public AudioMixerGroup audioMixer = null;

    public static AudioHandler AUDIO;
    public List <AudioHandler_Sound> sound = new List<AudioHandler_Sound>();

    private string currentScene;

    void Start()
    {
        AUDIO = this;

        //PlayOnStart
        for (int i = 0; i < sound.Count; i++)
        {
            //AudioSource
            if (sound[i].Settings.CreateAudioSource)
            {
                sound[i].Settings.AudioSource = this.gameObject.AddComponent<AudioSource>();
                sound[i].Settings.AudioSource.outputAudioMixerGroup = audioMixer;
                sound[i].Settings.AudioSource.outputAudioMixerGroup = sound[i].Settings.AudioMixerGroup;

            }

            //AudioClip
            sound[i].Settings.AudioSource.clip = sound[i].Settings.AudioClip;

            //Settings
            if (sound[i].AudioSettings.playOnStart)
            {
                sound[i].Settings.AudioSource.playOnAwake = sound[i].AudioSettings.playOnStart;
                sound[i].Settings.AudioSource.Play();
            }

        }

        RefreshSettings();
    }

    void Update()
    {
        CheckNewScene();

        if (refreshSettingsOnUpdate)
            RefreshSettings();

        if (refreshSettingsOnUpdate)
        {
            for (int i = 0; i < sound.Count; i++)
            {
                sound[i].Settings.AudioSource.volume = sound[i].AudioSettings.Volume;
            }
        }
    }

    private void CheckNewScene()
    {
        if (currentScene != SceneManager.GetActiveScene().name)
        {
            currentScene = SceneManager.GetActiveScene().name;
            for (int i = 0; i < sound.Count; i++)
            {
                for (int o = 0; o < sound[i].AudioControl.StartAudioOnScene.Count; o++)
                {
                    if (sound[i].AudioControl.StartAudioOnScene[o] == currentScene)
                    {
                        sound[i].Settings.AudioSource.Play();
                    }
                }
                for (int o = 0; o < sound[i].AudioControl.StopAudioOnScene.Count; o++)
                {
                    if (sound[i].AudioControl.StopAudioOnScene[o] == currentScene)
                    {

                    }
                }
            }
        }
    }

    public void PlayTrack(string trackname)
    {
        bool check = false;
        for (int i = 0; i < sound.Count; i++)
        {
            if (sound[i].AudioTrackName == trackname)
            {
                AudioHandler_PlayTrack(i);
                check = true;
            }
        }
        if (!check)
            Debug.Log(trackname + " cannot be found");
    }

    public void StartTrack(string trackname)
    {
        for (int i = 0; i < sound.Count; i++)
        {
            if (sound[i].AudioTrackName == trackname)
                if (!sound[i].Settings.AudioSource.isPlaying)
                    AudioHandler_PlayTrack(i);
        }
    }
    
    public void StopTrack(string trackname)
    {
        for (int i = 0; i < sound.Count; i++)
        {
            if (sound[i].AudioTrackName == trackname)
                sound[i].Settings.AudioSource.Stop();
        }
    }

    private void AudioHandler_PlayTrack(int trackid)
    {
        sound[trackid].Settings.AudioSource.Play();
    }
    public void RefreshSettings()
    {
        for (int i = 0; i < sound.Count; i++)
        {
            //SetClip
            if (sound[i].Settings.AudioSource.clip != sound[i].Settings.AudioClip)
                sound[i].Settings.AudioSource.clip = sound[i].Settings.AudioClip;

        }
    }

    public void SetAudioSource(string trackname, AudioSource audiosource)
    {
        for (int i = 0; i < sound.Count; i++)
        {
            if (sound[i].AudioTrackName == trackname)
                sound[i].Settings.AudioSource = audiosource;
        }
    }
}

[System.Serializable]
public class AudioHandler_Sound
{
    public string AudioTrackName;
    public AudioHandler_Settings Settings;
    public AudioHandler_AudioSettings AudioSettings;
    public AudioHandler_Control AudioControl;
}

[System.Serializable]
public class AudioHandler_Settings
{
    [Header("AudioClip/MixerGroup")]
    public AudioClip AudioClip;
    public AudioMixerGroup AudioMixerGroup;

    [Header("AudioSource")]
    public AudioSource AudioSource;
    public bool CreateAudioSource;
}

[System.Serializable]
public class AudioHandler_AudioSettings
{
    [Header("AudioSettings")]
    [Range(0, 1)] public float Volume = 1;
    public bool loop;
    public bool playOnStart;
}

[System.Serializable]
public class AudioHandler_Control
{
    [Header("Enable/Disable Song")]
    public List<string> StartAudioOnScene = new List<string>();
    public List<string> StopAudioOnScene = new List<string>();
    public bool stopOnNextScene;
    public int sceneEnabled;
}

