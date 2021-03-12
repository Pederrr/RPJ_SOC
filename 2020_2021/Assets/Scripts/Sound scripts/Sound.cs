using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip; 
    public string name;
    public bool theme;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public AudioSource source;

    public bool loop;

    public AudioMixerGroup audioMixerGroup;
}
