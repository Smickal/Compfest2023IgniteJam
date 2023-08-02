using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //REGISTER DIALOGUE
    //PLAYS DIALOGUE
    //DISPLAY NEXT DIALOGUES
    [SerializeField] ChatDialogueDisplay _chatDialogueDisplay;
    [SerializeField] Movement _movement;
    [SerializeField] CameraHandler _cameraHandler;

    List<DialogueChat> chatList = new List<DialogueChat>();

    private void Start()
    {
        _chatDialogueDisplay.OnButtonPressed += DisplayDialogues;
    }

    public void RegisterDialogues(Dialogue dialogue)
    {
        foreach (DialogueChat chat in dialogue.Chats) 
        {
            chatList.Add(chat);
        }


        DisplayDialogues();
    }

    void DisplayDialogues()
    {
        if (chatList.Count > 0)
        {
            _chatDialogueDisplay.DisplayString(chatList[0].text, chatList[0].textColor);
            chatList.RemoveAt(0);
        }
        else
        {
            _chatDialogueDisplay.DisableChatDialogue();

            _movement.ActivateMovement();
            _movement.StopFacingTarget();
            _cameraHandler.TriggerNormalFreeLookCamera();
        }
    }
}
