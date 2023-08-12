using UnityEngine;

public class TriggerActivateGameObject : MonoBehaviour
{
    [SerializeField] InteractedItem interactedItem;
    [SerializeField] GameObject _gameObject;



    public void RegisterTrigger()
    {
        interactedItem.OnTriggerAction += TriggerObject;
    }

    private void TriggerObject()
    {
        _gameObject.SetActive(true);
        interactedItem.OnTriggerAction -= TriggerObject;
    }

}
    