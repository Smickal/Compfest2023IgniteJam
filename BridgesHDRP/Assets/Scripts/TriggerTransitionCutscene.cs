using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TriggerTransitionCutscene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayableDirector _playableDirector;
    [SerializeField] TimelineAsset _transitionAsset;


    public void TriggerTransition()
    {
        _playableDirector.Play(_transitionAsset);
    }
}
