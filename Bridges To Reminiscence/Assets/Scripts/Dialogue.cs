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
    public string text;
    public Color textColor;
}

