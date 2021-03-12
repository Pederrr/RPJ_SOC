using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer SFXMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXslider;
    private Resolution[] resolutions;

    public void SetMusicVolume (float volume)
    {
        musicMixer.SetFloat("Master", Mathf.Log10(volume) * 20); //premena z lineranej škaly na decibely
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXvolume(float volume)
    {
        SFXMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetGraphics (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex); 
    }

    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void Awake()
    {
        resolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions(); // vymažem všetky možnosti v dropdowne

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height; // zmením na podporovaný formát
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) //tu zistím akú resolution má okno
            {
                currentResolutionIndex = i;
            } 
        }

        resolutionDropdown.AddOptions(options); // Pridám len možnosti, ktoré zariadenie podporuje
        resolutionDropdown.value = currentResolutionIndex; //tu nastavím aby sa v dropdowne zobrazovalo momentálne nastavenie
        resolutionDropdown.RefreshShownValue();

        int currentQualityIndex = QualitySettings.GetQualityLevel(); //tu zistím momentálne nastavenie kvality
        qualityDropdown.value = currentQualityIndex; //tu nastavím aby sa v dropdowne zobrazovalo momentálne nastavenie
        qualityDropdown.RefreshShownValue();

        fullScreenToggle.isOn = Screen.fullScreen; //tu zistím, ci je fullscreen zapnuty -- ak hej, tak je "zapnuty" aj toggle

        try
        {
            float vol = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = vol;
        }
        catch
        {
            musicSlider.value = 1;
        }

        try
        {
            float vol = PlayerPrefs.GetFloat("SFXVolume");
            SFXslider.value = vol;
        }
        catch
        {
            SFXslider.value = 1;
        }
    }
}
