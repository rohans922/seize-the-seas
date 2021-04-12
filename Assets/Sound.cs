using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
