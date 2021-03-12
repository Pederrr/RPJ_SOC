using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer SFXMixer;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("destroying");
            Destroy(gameObject);
            return;

        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();        
            s.source.clip = s.clip;   
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }

        try
        {
            float vol = PlayerPrefs.GetFloat("MusicVolume");
            musicMixer.SetFloat("Master", Mathf.Log10(vol) * 20);
            vol = PlayerPrefs.GetFloat("SFXVolume");
            SFXMixer.SetFloat("Master", Mathf.Log10(vol) * 20);
        }
        catch
        {
            
        }
    }

    private void Start()
    {
        Play("theme", true); 
    }

    public void Play (string name, bool play)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Bad sound name");
            return;
        }
        if (play)
        {
            if (s.theme)
            {
                s.source.Play();
            }
            else
            {
                s.source.PlayOneShot(s.clip); //na efekty je lepšie playOneShot
            }
        }
        else
        {
            s.source.Stop();
        }
    }
}
