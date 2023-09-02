using Knife.HDRPOutline.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractedItem : MonoBehaviour, IInteractable
{
    [SerializeField] float checkDistance = 1f;
    [Space(10)]
    [SerializeField] Collider _playerCollider;
    [SerializeField] LayerMask _playerLayerMask;

    [Space(10)]
    [Header("DialogueDetails")]
    [SerializeField] Dialogue _dialogueData;
    [SerializeField] Transform _conversationTransform;
    [SerializeField] InteractManager _interactManager;

    [Space(10)]
    [Header("Outline")]
    [SerializeField] OutlineObject[] _outlineSystem;

    [Space(10)]
    [SerializeField] bool ActivatedOutline = true;
    [SerializeField] bool isThisKeyItem;
    [SerializeField] bool isThisADialogue = true;

    [Space(5)]
    [SerializeField] List<TriggerActivateGameObject> _allTriggerItem;

    public event Action OnTriggerAction;


    
    bool IsTriggered = false;
    bool isInteracted = false;
    public Dialogue DialogueData { get { return _dialogueData; } }
    public Transform ConversationTransform { get { return _conversationTransform; } }
    public bool IsInteracted { get { return isInteracted; } }
    public List<TriggerActivateGameObject> TriggerActivateGameObjects { get { return _allTriggerItem; } }
    public bool IsThisKeyItem { get { return isThisKeyItem; } }

    private void Update()
    {
        if (IsTriggered)
        {
            Vector3 colliderPos = _playerCollider.transform.position;
            Vector3 playerPos = transform.position;

            colliderPos.y = 0f;
            playerPos.y = 0f;

            float distance = Vector3.Distance(colliderPos, playerPos);

            if (distance > checkDistance)
            {
                DeactivateOutline();
            }

            if(isInteracted) DeactivateOutline();
        }


    }


    public void TriggerInteract()
    {
        OnTriggerAction?.Invoke();
    }


    public void ActivateOutline()
    {
        if (IsTriggered) return;

        foreach(var outline in _outlineSystem)
        {
            outline.enabled = true;
        }

        IsTriggered = true;
        //Debug.Log("activate outline + " + gameObject.name);
    }

    public void DeactivateOutline()
    {
        if(!IsTriggered) return;

        foreach(var outine in _outlineSystem)
        {
            outine.enabled = false;
        }

        IsTriggered = false;
        //Debug.Log("OFf outline + " + gameObject.name);
    }

    public bool IsThisADialogueinteract()
    {
        return isThisADialogue;
    }

    public void Interacted()
    {
        isInteracted = true;
    }

    public void IncreaseTrigger()
    {
        if (isThisKeyItem)
            _interactManager.IncreaseInteractCount();
    }

}
