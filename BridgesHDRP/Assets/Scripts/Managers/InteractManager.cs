using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class InteractManager: MonoBehaviour
{


    [SerializeField] PercentageManager percentageManager;
    [SerializeField] InteractedItem[] interactedItems;
    int interactedCount = 0;


    bool allItemIsInteracted = false;

    public bool IsAllItemInteracted { get {  return allItemIsInteracted; } }

    private void Start()
    {
        percentageManager.DisplayPercentage(interactedCount, interactedItems.Length);
    }

    public void IncreaseInteractCount()
    {
        interactedCount++;
        CheckForInteractedItem();
        percentageManager.DisplayPercentage(interactedCount, interactedItems.Length);
    }


    private void CheckForInteractedItem()
    {
        if(interactedCount >= interactedItems.Length)
        {
            allItemIsInteracted = true;
        }
    }

}