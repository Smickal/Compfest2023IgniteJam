using System;
using UnityEngine;

public class InteractManager: MonoBehaviour
{
    [SerializeField] InteractedItem[] interactedItems;
    int interactedCount = 0;


    public event Action OnAllItemInteracted;


    public void IncreaseInteractCount()
    {
        interactedCount++;
        CheckForInteractedItem();
    }


    private void CheckForInteractedItem()
    {
        if(interactedCount >= interactedItems.Length)
        {
            OnAllItemInteracted?.Invoke();
            Debug.Log("All Item Interacted");
        }
    }


}