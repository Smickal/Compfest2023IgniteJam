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

    [Space(6)]
    [Header("RaycastProperties")]
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private LayerMask _rayLayerMask;

    Collider[] _checkedColliders;
    InteractedItem item;
    List<TriggerActivateGameObject> allTrigger;

    Collider closestCollider;


    bool isItemObstructed = false;
    bool needSearch = true;
    bool interactPressed = false;

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
            if (closestCollider == null) return;

            Vector3 rayDir = closestCollider.transform.position - _rayOrigin.position;
            RaycastHit rayHitInfo = new RaycastHit();
            Physics.Raycast(_rayOrigin.position, rayDir, out rayHitInfo, 10f, _rayLayerMask);
            
            //Check if there's any thing obstructed (to prevent wall checking)
            if (rayHitInfo.collider != closestCollider) return;

            item = null;
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
            FindObjectOfType<AudioManager>().PlaySound("Interact");

        //Trigger Isi Itemnya
        item.TriggerInteract();

        if (item.IsThisADialogueinteract() == false || item.IsInteracted == true || interactPressed == true) return;
        interactPressed = true;
        needSearch = false;
        StartCoroutine(StartDialogue());
        
    }


    IEnumerator StartDialogue()
    {
        allTrigger = item.TriggerActivateGameObjects;
        _blinkAndFadeManager.TriggerFadeToBlack();

        Vector3 movTowardsPos = item.ConversationTransform.position;
        yield return new WaitForSeconds(0.4f);
        interactPressed = false;
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
