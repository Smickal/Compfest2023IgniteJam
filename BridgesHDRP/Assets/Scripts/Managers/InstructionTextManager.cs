using System.CodeDom;
using System.Collections;
using TMPro;
using UnityEngine;

public class InstructionTextManager : MonoBehaviour
{
    const float timeToFade = 5f;
    
    [SerializeField] TMP_Text _instructionText;
    [SerializeField] Animation _animation;
    [SerializeField] GameObject _instructionObj;

    [Header("Animation")]
    [SerializeField] AnimationClip _fadeInClip;
    [SerializeField] AnimationClip _fadeOutClip;

    public void TriggerInstructionText(string instructionText)
    {
        _instructionObj.SetActive(true);
        _instructionText.SetText(instructionText);

        _animation.clip = _fadeInClip;
        _animation.Play();

        StopAllCoroutines();
        StartCoroutine(StartOffCountdown());
    }

    IEnumerator StartOffCountdown()
    {
        yield return new WaitForSeconds(timeToFade);
        _animation.clip = _fadeOutClip;
        _animation.Play();

        yield return new WaitForSeconds(timeToFade);
        _instructionObj.SetActive(false);
    }
}
