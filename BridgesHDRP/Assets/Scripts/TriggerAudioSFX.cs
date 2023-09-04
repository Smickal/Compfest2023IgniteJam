using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudioSFX : TriggerActivateGameObject
{
    [Space(10)]
    [SerializeField] AudioClip _audioClip;

    public override void RegisterTrigger()
    {
        interactedItem.OnTriggerAction += Trigger;
    }

    private void Trigger()
    {
        FindObjectOfType<AudioManager>().PlaySound(_audioClip);
        interactedItem.OnTriggerAction -= Trigger;
    }
}
