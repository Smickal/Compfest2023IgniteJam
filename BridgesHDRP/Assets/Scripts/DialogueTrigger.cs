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

    List<DialogueChat> chatList = new List<DialogueChat>();

    bool isCutsceneSentence = false;

    private void Start()
    {
        _chatDialogueDisplay.OnButtonPressed += DisplayDialogues;
        _chatDialogueDisplay.OnSentenceDone += RemoveSentenceInZeroIndex;
    }

    public void RegisterDialogues(Dialogue dialogue)
    {
        if (dialogue == null) return;
        chatList.Clear();
        _movement.DisableMovement();

        foreach (DialogueChat chat in dialogue.Chats) 
        {
            chatList.Add(chat);
        }

        isCutsceneSentence = false;
        DisplayDialogues();
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
                _chatDialogueDisplay.DisplayCutsceneString(chatList[0].Text);
            }



            if (chatList[0].IsCameraSpecial)
            {
                _cameraHandler.MoveInpectCameraToCustomLoc(chatList[0].CameraPos, chatList[0].CameraRotation);
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

            _movement.ActivateMovement();
            _movement.StopFacingTarget();
            _cameraHandler.TriggerNormalFreeLookCamera();
        }
    }


    public void RemoveSentenceInZeroIndex()
    {
        if (chatList[0].TriggerSomethingHere) _interact.TriggerWhateverInsideItem();
        chatList.RemoveAt(0);
    }
}
