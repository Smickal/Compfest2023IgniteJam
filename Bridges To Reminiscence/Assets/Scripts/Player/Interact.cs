using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    const float _interactCheckRadius = 1f;    
    
    [SerializeField] private LayerMask _inspectItemLayerMask;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CameraHandler _cameraHandler;
    [SerializeField] private Movement _movement;
    [SerializeField] private DialogueTrigger _dialogueTrigger;

    Collider[] _checkedColliders;

    private void Start()
    {
        _inputReader.OnInteractPressed += CheckInteract;
    }


    private void CheckInteract()
    {
        _checkedColliders = Physics.OverlapSphere(transform.position, _interactCheckRadius, _inspectItemLayerMask);
        if (_checkedColliders.Length == 0) return;
        Collider closestCollider = SearchForClosestItem();

        //Trigger Isi Itemnya
        InteractedItem item = closestCollider.GetComponent<InteractedItem>();

        item.TriggerInteract();

        _cameraHandler.TriggerInteractCamera();
        //Disable Movement and Face Target
        _movement.DisableMovement();
        _movement.FaceTarget(item.transform);

        //TriggerDialogue
        _dialogueTrigger.RegisterDialogues(item.DialogueData);
    }


    private Collider SearchForClosestItem()
    {
        float closestDistance = float.MaxValue;
        if(_checkedColliders.Length == 1) return _checkedColliders[0];

        Collider closestItem = null;

        foreach(Collider collider in _checkedColliders)
        {
            float tempDistance = Vector3.Distance(transform.position, collider.transform.position);
            if ( tempDistance < closestDistance)
            {
                closestDistance = tempDistance;
                closestItem = collider;
            }
        }

        return closestItem;
    }

}
