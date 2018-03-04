using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

	public AudioMixer audioMixer;
	public Slider slider;

	void Start(){
		slider.normalizedValue = PlayerPrefs.GetFloat("volume", 1);
	}

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
