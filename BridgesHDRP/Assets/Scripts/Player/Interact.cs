using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    const float _interactCheckRadius = 1.5f;    
    
    [SerializeField] private LayerMask _inspectItemLayerMask;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CameraHandler _cameraHandler;
    [SerializeField] private Movement _movement;
    [SerializeField] private DialogueTrigger _dialogueTrigger;

    Collider[] _checkedColliders;
    InteractedItem item;
    List<TriggerActivateGameObject> allTrigger;

    Collider closestCollider;

    private void Start()
    {
        _inputReader.OnInteractPressed += CheckInteract;
    }

    private void Update()
    {
        _checkedColliders = Physics.OverlapSphere(transform.position, _interactCheckRadius, _inspectItemLayerMask);

        closestCollider = SearchForClosestItem();
        item = null;
        if (closestCollider == null) return;
        item = closestCollider.GetComponent<InteractedItem>();

        item.ActivateOutline();
    }



    private void CheckInteract()
    {      
        if (item == null) return;
       

        //Trigger Isi Itemnya
        item.TriggerInteract();

        if(item.IsThisADialogueinteract() == false || item.IsInteracted == true) return;

        allTrigger = item.TriggerActivateGameObjects;

        Vector3 movTowardsPos = item.ConversationTransform.position;

        _movement.SetMoveTowardDestination(movTowardsPos);
        
        _cameraHandler.TriggerInteractCamera(item.transform);
        //Disable Movement and Face Target
        _movement.DisableMovement();
        _movement.FaceTarget(item.transform);

        //TriggerDialogue
        _dialogueTrigger.RegisterDialogues(item.DialogueData);
        item.Interacted();
    }

    public void TriggerWhateverInsideItem()
    {
        allTrigger[0].RegisterTrigger();
        item.TriggerInteract();

        allTrigger.RemoveAt(0);
    }


    private Collider SearchForClosestItem()
    {
        float closestDistance = float.MaxValue;
        if(_checkedColliders.Length == 1) return _checkedColliders[0];

        Collider closestItem = null;

        foreach(Collider collider in _checkedColliders)
        {
            float tempDistance = Vector3.Distance(transform.position, collider.transform.position);

            if (collider.GetComponent<InteractedItem>().IsInteracted == true) continue;

            if (tempDistance < closestDistance)
            {
                closestDistance = tempDistance;
                closestItem = collider;
            }
        }

        return closestItem;
    }

}
