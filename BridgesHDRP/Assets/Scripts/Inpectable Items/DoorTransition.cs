using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTransition : MonoBehaviour
{
    // Start is called before the first frame update]
    [SerializeField] InteractedItem _interactedItem;
    [SerializeField] TransitionManager _transitionManager;
    

    private void Start()
    {
        _interactedItem.OnTriggerAction += TriggerNextScene;
    }


    private void TriggerNextScene()
    {
        _transitionManager.TriggerTransition();
    }

}
