using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

	public AudioMixer audioMixer;

    public void SetVolume (float volume)
	{
        //audioMixer.SetFloat ("MasterVolume", volume);
        PlayerPrefs.SetFloat("volume", volume);
	}

    void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1);
    }
}
