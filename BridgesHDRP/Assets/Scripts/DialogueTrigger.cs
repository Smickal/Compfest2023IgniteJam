using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //REGISTER DIALOGUE
    //PLAYS DIALOGUE
    //DISPLAY NEXT DIALOGUES
    [SerializeField] Interact _interact;
    [SerializeField] ChatDialogueDisplay _chatDialogueDisplay;
    [SerializeField] Movement _movement;
    [SerializeField] CameraHandler _cameraHandler;
    [SerializeField] InteractManager _interactManager;

    List<DialogueChat> chatList = new List<DialogueChat>();

    bool isCutsceneSentence = false;
    bool isFirstTrigger = false;
    InteractedItem item;
    

    private void Start()
    {
        _chatDialogueDisplay.OnButtonPressed += DisplayDialogues;
        _chatDialogueDisplay.OnSentenceDone += RemoveSentenceInZeroIndex;
    }

    public void RegisterDialogues(InteractedItem item)
    {
        if (item.DialogueData == null) return;
        this.item = item;
        Dialogue dialogue = item.DialogueData;

        chatList.Clear();
        _movement.DisableMovement();

        foreach (DialogueChat chat in dialogue.Chats) 
        {
            chatList.Add(chat);
        }

        isCutsceneSentence = false;

        if (chatList[0].IsCameraSpecial)
        {
            _cameraHandler.MoveInpectCameraToCustomLoc(chatList[0].CameraPos);
        }
        else
        {
            _cameraHandler.ResetInspectCameraPosition();
        }
    }

    public void RegisterCutsceneDialogues(Dialogue dialogue)
    {
        chatList.Clear();


        foreach (DialogueChat chat in dialogue.Chats)
        {
            chatList.Add(chat);
        }

        isCutsceneSentence = true;
        DisplayDialogues();
    }



    public void DisplayDialogues()
    {
        if (chatList.Count > 0)
        {

            if (!isCutsceneSentence)
            {
                if (isFirstTrigger == true)
                    FindObjectOfType<AudioManager>().PlaySound("ChatNext");

                isFirstTrigger = true;
                if(_chatDialogueDisplay.IsSentenceDone)
                {
                    _chatDialogueDisplay.DisplayString(chatList[0].Text, chatList[0].TextColor);
                }
                else
                {
                    _chatDialogueDisplay.DisplayDoneString(chatList[0].Text, chatList[0].TextColor);
                }
            }
            else
            {
                _chatDialogueDisplay.DisplayCutsceneString(chatList[0].Text, chatList[0].TextColor);
            }



            if (chatList[0].IsCameraSpecial)
            {
                _cameraHandler.MoveInpectCameraToCustomLoc(chatList[0].CameraPos);
            }
            else
            {
                _cameraHandler.ResetInspectCameraPosition();
            }

            
            if(_chatDialogueDisplay.IsSentenceDone)
            {
                RemoveSentenceInZeroIndex();
            }
        }
        else
        {
            _chatDialogueDisplay.DisableChatDialogue();

           
            _movement.StopFacingTarget();
            _cameraHandler.TriggerNormalFreeLookCamera();

            _interact.ActivateSearch();

            Invoke("EnableMovement", 1.9f);

            isFirstTrigger = false;

            if (item == null) return;
            if(item.IsThisKeyItem)
            {
                _interactManager.IncreaseInteractCount();
                item = null;
            }
        }
    }

    private void EnableMovement()
    {
        _movement.ActivateMovement();
    }
    public void RemoveSentenceInZeroIndex()
    {
        if (chatList[0].TriggerSomethingHere) _interact.TriggerWhateverInsideItem();
        chatList.RemoveAt(0);
    }
}
