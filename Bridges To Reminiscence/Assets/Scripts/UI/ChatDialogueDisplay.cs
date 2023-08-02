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

    const float wordSpeed = 0.025f;
    StringBuilder sb = new StringBuilder();

    public event Action OnButtonPressed;


    private void Start()
    {
        _button.onClick.AddListener(() => OnButtonPressed?.Invoke());
    }


    public void DisplayString(string text, Color color)
    {
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
        sb.Clear();
        foreach (char character in text)
        {
            sb.Append(character);
            _tmpText.SetText(sb);

            yield return new WaitForSeconds(wordSpeed);
        }
    }


}
