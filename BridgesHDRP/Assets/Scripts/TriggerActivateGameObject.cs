using UnityEngine;

public class TriggerActivateGameObject : MonoBehaviour
{
    [SerializeField] protected InteractedItem interactedItem;
    [SerializeField] GameObject _gameObject;



    public virtual void RegisterTrigger()
    {
        interactedItem.OnTriggerAction += TriggerObject;
    }

    private void TriggerObject()
    {
        _gameObject.SetActive(true);
        interactedItem.OnTriggerAction -= TriggerObject;
    }
}
    