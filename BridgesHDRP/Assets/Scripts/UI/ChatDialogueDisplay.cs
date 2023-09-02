using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatDialogueDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button _button;
    [SerializeField] TextMeshProUGUI _tmpText;

    [Space(10)]
    [SerializeField] GameObject _dropdownOBJ;
    [SerializeField] GameObject _chatGO;

    bool isSentenceDone = true;

    const float wordSpeed = 0.01f;
    StringBuilder sb = new StringBuilder();
    public bool IsSentenceDone { get { return isSentenceDone; } }

    public event Action OnButtonPressed;
    public event Action OnSentenceDone;

    private void Start()
    {
        _button.onClick.AddListener(() => OnButtonPressed?.Invoke());
    }

    public void DisplayString(string text, Color color)
    { 
        _dropdownOBJ.SetActive(true);
        _button.enabled = true;

        _chatGO.SetActive(true);
        _tmpText.color = color;
        StartCoroutine(PlayString(text));  
    }

    public void DisplayDoneString(string text, Color color)
    {
        _dropdownOBJ.SetActive(true);
        _button.enabled = true;

        StopAllCoroutines();
        _tmpText.text = text;
        _tmpText.color = color;

        isSentenceDone = true;
    }


    public void DisplayCutsceneString(string text, Color color)
    {
        _dropdownOBJ?.SetActive(false);
        _button.enabled = false;    

        _chatGO.SetActive(true);
        _tmpText.color = color;


        StartCoroutine(PlayString(text));
    }

    public void DisableChatDialogue()
    {
        _chatGO?.SetActive(false);
    }

    IEnumerator PlayString(string text)
    {
        isSentenceDone = false;
        sb.Clear();
        foreach (char character in text)
        {
            sb.Append(character);
            _tmpText.SetText(sb);

            yield return new WaitForSeconds(wordSpeed);
        }

        isSentenceDone = true;
        OnSentenceDone?.Invoke();
    }



}
