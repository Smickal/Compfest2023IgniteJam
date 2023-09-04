using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;

    [Range(0,1)]public float Volume = 1;
    [Range(1,3)]public float Pitch = 1;

    [HideInInspector] public AudioSource Source;
    public bool PlayOnAwake;
    public bool Loop;
}
