using System;
using UnityEngine;

public class InteractManager: MonoBehaviour
{
    [SerializeField] InteractedItem[] interactedItems;
    int interactedCount = 0;


    bool allItemIsInteracted = false;

    public bool IsAllItemInteracted { get {  return allItemIsInteracted; } }


    public void IncreaseInteractCount()
    {
        interactedCount++;
        CheckForInteractedItem();
    }


    private void CheckForInteractedItem()
    {
        if(interactedCount >= interactedItems.Length)
        {
            allItemIsInteracted = true;
        }
    }


}