using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;

            s.source.loop = s.loop;
        }
    }

    void Start() {
        Play("Background");
    }

    public void Play (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("Sound " + name+ " not found");
            return;
        }
        s.source.Play();
        //FindObjectOfType<AudioManager>().Play("nameofsound");
    }

    public void ToggleAll() {
        foreach (Sound s in sounds) {
            s.source.mute = !s.source.mute;
        }
    }

    public void ToggleMusic() {
        foreach (Sound s in sounds) {
            if (s.name == "Background")
                s.source.mute = !s.source.mute;
        }
    }

    public void ToggleSFX() {
        foreach (Sound s in sounds) {
            if(s.name != "Background")
                s.source.mute = !s.source.mute;
        }
    }

    public void ChangeMusicVolume(float volume) {
        Debug.Log("Changing music volume to " + volume);
        foreach (Sound s in sounds) {
            if (s.name == "Background")
                s.source.volume = volume;
        }
    }
    public void ChangeSFXVolume(float volume) {
        foreach (Sound s in sounds) {
            if (s.name != "Background")
                s.source.volume = volume;
        }
    }
}
