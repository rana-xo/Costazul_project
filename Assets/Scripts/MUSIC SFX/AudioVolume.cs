using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class AudioVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
        LoadSettings();
    }

    void LoadSettings()
    {
        float volume = PlayerPrefs.GetFloat("Volume");

        SetVolume(volume);
    }

    public void SetVolume(float value)
    {
        mixer.SetFloat("Volume", value);
        volumeSlider.value = value;
        PlayerPrefs.SetFloat("Volume", value);
    }
}
