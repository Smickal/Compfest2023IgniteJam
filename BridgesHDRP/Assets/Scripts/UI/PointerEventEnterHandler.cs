using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEventEnterHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator _animator;
    int TriggerDropDown = Animator.StringToHash("Highlighted");
    int Normal = Animator.StringToHash("Normal");

    private void Start()
    {      
        _animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetTrigger(TriggerDropDown);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetTrigger(Normal);
    }
}
