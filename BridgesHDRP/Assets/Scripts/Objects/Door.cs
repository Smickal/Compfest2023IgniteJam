using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    int doorOpenToRight = Animator.StringToHash("OpenDoorToRight");
    int doorOpenToLeft = Animator.StringToHash("OpenDoorToLeft");

    [SerializeField] InteractedItem _interactedItem;
    [SerializeField] Animator _animator;

    [Space(10)]
    [SerializeField] bool isRight = true;
    [SerializeField] bool isRegularDoor = true;

    bool isDoorOpened = false;
    private void Start()
    {
       if(isRegularDoor) _interactedItem.OnTriggerAction += OpenDoor;
    }

    public void OpenDoor()
    {
        if (isDoorOpened) return;

        if(isRight)
        {
            _animator.SetTrigger(doorOpenToRight);
        }
        else
        {
            _animator.SetTrigger(doorOpenToLeft);
        }

        isDoorOpened = true;
    }

}
