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

    Collider[] _checkedCollider;

    float checkDistance = 1f;
    bool IsTriggered = false;

    public Dialogue DialogueData { get { return _dialogueData; } }


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
        Debug.Log("Interacted with " + gameObject.name);
    }


    private void ActivateOutline()
    {
        gameObject.layer = LayerMask.NameToLayer(outlineLayerString);
        IsTriggered = true;

    }

    private void DeactivateOutline()
    {
        gameObject.layer = LayerMask.NameToLayer(defaultLayerString);
        IsTriggered = false;

    }
}
