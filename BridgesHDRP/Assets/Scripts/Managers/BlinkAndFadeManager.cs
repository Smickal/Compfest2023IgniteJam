using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAndFadeManager : MonoBehaviour
{
    int TriggerBlinkHash = Animator.StringToHash("TriggerBlink");
    int TriggerFadeToBlackHash = Animator.StringToHash("TriggerFadeToBlack");
    
    
    [SerializeField] Animator _animator;
    public void TriggerBlink()
    {
        _animator.SetTrigger(TriggerBlinkHash);
    }

    public void TriggerFadeToBlack()
    {
        _animator.SetTrigger(TriggerFadeToBlackHash);
    }

    
}
