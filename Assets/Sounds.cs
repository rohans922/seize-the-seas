using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public Sound[] all_sounds;
    public int pl_num;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in all_sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
            if (pl_num == 1) {
                s.source.panStereo = -0.3f;
            } else {
                s.source.panStereo = 0.3f;
            }
        }
    }

    public void PlaySound(string name)
    {
        Sound a = Array.Find(all_sounds, sound => sound.name == name);
        a.source.Play();
    }
    public void StopSound(string name)
    {
        Sound a = Array.Find(all_sounds, sound => sound.name == name);
        a.source.Stop();
    }

    public void StopAll()
    {
        foreach (Sound s in all_sounds)
        {
            s.source.Stop();
        }
    }
}
