using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    private AudioSource privateSource;
    private void Awake()
    {
        privateSource = gameObject.AddComponent<AudioSource>();

        foreach(var sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.playOnAwake = sound.PlayOnAwake;
            sound.Source.spatialBlend = 0f;
            sound.Source.loop = sound.Loop;


            if (sound.PlayOnAwake) PlaySound(sound.Name);
        }
    }

    public void PlaySound(string name)
    {
        Sound soundTemp = Array.Find(Sounds, sound => sound.Name == name);
        soundTemp.Source.Play();
    }
    
    public void PlaySound(AudioClip audioClip)
    {
        privateSource.PlayOneShot(audioClip);
    }

    public void StopPlayingPrivateSource()
    {
        privateSource.Stop();
    }


}
