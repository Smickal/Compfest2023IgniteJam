using Knife.HDRPOutline.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractedItem : MonoBehaviour, IInteractable
{
    const string outlineLayerString = "Outlined";
    const string defaultLayerString = "Inspectable";

    
    [SerializeField] Collider _playerCollider;
    [SerializeField] LayerMask _playerLayerMask;
    [SerializeField] Dialogue _dialogueData;
    [SerializeField] Transform _conversationTransform;
    [SerializeField] InteractManager _interactManager;
    [SerializeField] OutlineObject _outlineSystem;
    [SerializeField] bool isThisKeyItem;

    [Space(5)]
    [SerializeField] List<TriggerActivateGameObject> _allTriggerItem;

    public event Action OnTriggerAction;

    Collider[] _checkedCollider;

    float checkDistance = 1f;
    bool IsTriggered = false;
    bool isInteracted = false;
    public Dialogue DialogueData { get { return _dialogueData; } }
    public Transform ConversationTransform { get { return _conversationTransform; } }
    public bool IsInteracted { get { return isInteracted; } }
    public List<TriggerActivateGameObject> TriggerActivateGameObjects { get { return _allTriggerItem; } }


    void Update()
    {
        _checkedCollider = Physics.OverlapSphere(transform.position, checkDistance, _playerLayerMask);

        if(_checkedCollider.Length == 0 && IsTriggered)
        {
            DeactivateOutline();
        }

       
        if(_checkedCollider.Length > 0 && !IsTriggered)
        {
            ActivateOutline();
        }
    }

    

    public void TriggerInteract()
    {
        OnTriggerAction?.Invoke();
    }


    private void ActivateOutline()
    {
        _outlineSystem.enabled = true;
        IsTriggered = true;

    }

    private void DeactivateOutline()
    {
        _outlineSystem.enabled = false;
        IsTriggered = false;

    }

    public void InteractedWithObject()
    {
        isInteracted = true;
        if(isThisKeyItem)
         _interactManager.IncreaseInteractCount();
    }
}
