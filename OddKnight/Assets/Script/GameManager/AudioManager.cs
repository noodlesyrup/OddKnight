using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    void Awake()
    {
        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop; 
        }
    }

    private void Start()
    {
        Play("BGM1");
        Play("BGM2");
        Play("MenuTheme");
    }

    public void Play (String name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) 
           return;
        s.source.Play();
    }

}
