using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTransition : MonoBehaviour
{
    // Start is called before the first frame update]
    [SerializeField] InteractedItem _interactedItem;
    [SerializeField] TransitionManager _transitionManager;
    [SerializeField] bool isMenuTransition = false;

    private void Start()
    {
        if(isMenuTransition == false)
        {
            _interactedItem.OnTriggerAction += () => { _transitionManager.TriggerTransition(); };
        }
        else
        {
            _interactedItem.OnTriggerAction += () => { _transitionManager.TriggerBackToMenu(); };
        }
    }

}
