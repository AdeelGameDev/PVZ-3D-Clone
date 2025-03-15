using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;


    protected override void Awake()
    {
      base.Awake();

        foreach (Sound s in sounds)
        {
            s.source= gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Bg");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds,sound => sound.name==name); // Needs Understanding

        if (s == null)
        {
            Debug.LogWarning("Sound : " + name+ " not found!");
            return;
        }
            
        s.source.Play();
    }

}
