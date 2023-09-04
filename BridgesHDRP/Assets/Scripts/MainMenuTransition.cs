using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTransition : MonoBehaviour
{
    // Start is called before the first frame update
    int animatorHash = Animator.StringToHash("TriggerOpening");


    [SerializeField] Animator _animator;
    [SerializeField] TransitionManager transitionManager;
        
    public void TriggerOpeningCutscene()
    {
        _animator.SetTrigger(animatorHash);
    }

    public void TriggerNextScene()
    {
        transitionManager.ForceNextScene();
    }
}
