using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    const string ErrorText = "The Door is not responding!";
    const float transitionTime = 1f;

    int TriggerTransitionHash = Animator.StringToHash("TriggerTransition");
    int TriggerUnTransitionHash = Animator.StringToHash("TriggerUnTransition");


    [SerializeField] Animator _animator;
    [SerializeField] InteractManager _interactManager;
    [SerializeField] InstructionTextManager _instructionTextManager;

    [Space(5)] 
    [SerializeField] bool isActivatedAtBeginning = false;

    private void Start()
    {
        if(isActivatedAtBeginning)
        {
            TriggerOpening();
        }
    }

    public void TriggerTransition()
    {
        if(_interactManager.IsAllItemInteracted != true)
        {
            _instructionTextManager.TriggerInstructionText(ErrorText);
            return;
        }

        _animator.SetTrigger(TriggerTransitionHash);
        StartCoroutine(NextScene());
    }

    public void TriggerOpening()
    {
        _animator.SetTrigger(TriggerUnTransitionHash);
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
