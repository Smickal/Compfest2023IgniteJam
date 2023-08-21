using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] DialogueChat[] _chats;

    public DialogueChat[] Chats { get { return _chats; } }
}

[System.Serializable]
public class DialogueChat
{
    [field: TextArea(2,7)]public string Text;
    public bool TriggerSomethingHere = false;

    public Color TextColor;

    [field: Header("Camera")]
    public bool IsCameraSpecial = false;
    public Vector3 CameraPos;
    public Vector3 CameraRotation;
}
