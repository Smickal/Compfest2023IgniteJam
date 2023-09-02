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
    [SerializeField] private BlinkAndFadeManager _blinkAndFadeManager;

    Collider[] _checkedColliders;
    InteractedItem item;
    List<TriggerActivateGameObject> allTrigger;

    Collider closestCollider;


    bool needSearch = true;

    private void Start()
    {
        _inputReader.OnInteractPressed += CheckInteract;
    }

    private void Update()
    {
        if(needSearch == true)
        {
            _checkedColliders = Physics.OverlapSphere(transform.position, _interactCheckRadius, _inspectItemLayerMask);

            closestCollider = SearchForClosestItem();
            item = null;
            if (closestCollider == null) return;
            item = closestCollider.GetComponent<InteractedItem>();

            if (item.IsInteracted == true) return;
            item.ActivateOutline();
        }
    }



    private void CheckInteract()
    {

        if (item == null) return;
        _checkedColliders = Physics.OverlapSphere(transform.position, _interactCheckRadius, _inspectItemLayerMask);

        InteractedItem tempItem = SearchForClosestItem().GetComponent<InteractedItem>();

        if (tempItem != item) return;

        //Trigger Isi Itemnya
        item.TriggerInteract();

        if (item.IsThisADialogueinteract() == false || item.IsInteracted == true) return;
        needSearch = false;
        StartCoroutine(StartDialogue());
        
    }


    IEnumerator StartDialogue()
    {
        allTrigger = item.TriggerActivateGameObjects;
        _blinkAndFadeManager.TriggerFadeToBlack();

        Vector3 movTowardsPos = item.ConversationTransform.position;
        yield return new WaitForSeconds(0.4f);

        _movement.SetMoveTowardDestination(movTowardsPos);

        
        //Disable Movement and Face Target
        _movement.DisableMovement();
        _movement.FaceTarget(item.transform);

        _dialogueTrigger.RegisterDialogues(item);
        yield return new WaitForSeconds(0.3f);

        _cameraHandler.TriggerInteractCamera(item.transform);
        yield return new WaitForSeconds(0.6f);

        _dialogueTrigger.DisplayDialogues();
        //TriggerDialogue
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

    public void ActivateSearch()
    {
        needSearch = true;
    }
}
