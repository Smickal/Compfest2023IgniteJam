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
    [Space(5)]
    [SerializeField] bool ActivatedOutline = true;
    [SerializeField] bool isThisKeyItem;
    [SerializeField] bool isThisADialogue = true;

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

    private void Update()
    {
        if(IsTriggered)
        {
            float distance = Vector3.Distance(_playerCollider.transform.position, transform.position);

            if(distance > checkDistance)
            {
                DeactivateOutline();
            }
        }
    }


    public void TriggerInteract()
    {
        OnTriggerAction?.Invoke();
        
        if (isThisKeyItem)
            _interactManager.IncreaseInteractCount();
    }


    public void ActivateOutline()
    {
        if (IsTriggered) return;

        _outlineSystem.enabled = true;
        IsTriggered = true;

    }

    public void DeactivateOutline()
    {
        if(!IsTriggered) return;

        _outlineSystem.enabled = false;
        IsTriggered = false;
    }

    public bool IsThisADialogueinteract()
    {
        return isThisADialogue;
    }

    public void Interacted()
    {
        isInteracted = true;
    }

}
