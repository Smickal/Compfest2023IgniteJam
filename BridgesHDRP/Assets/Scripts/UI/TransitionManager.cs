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
    int TriggerEndingHash = Animator.StringToHash("TriggerEnding");

    [SerializeField] Animator _animator;
    [SerializeField] InteractManager _interactManager;
    [SerializeField] InstructionTextManager _instructionTextManager;
    [SerializeField] Door _door;
    [SerializeField] AudioClip _teleport;

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

        if(_door != null)
        {
            _door.OpenDoor();
        }

        _animator.SetTrigger(TriggerTransitionHash);
        StartCoroutine(NextScene());
    }

    public void ForceNextScene()
    {
        StartCoroutine(NextScene());
    }

    public void TriggerBackToMenu()
    {
        if (_interactManager.IsAllItemInteracted != true)
        {
            _instructionTextManager.TriggerInstructionText(ErrorText);
            return;
        }

        if (_door != null)
        {
            _door.OpenDoor();
        }
        _animator.SetTrigger(TriggerEndingHash);
        StartCoroutine(LoadMenu());
    }


    public void TriggerOpening()
    {
        _animator.SetTrigger(TriggerUnTransitionHash);
        FindObjectOfType<AudioManager>().PlaySound("Teleport");
    }

    IEnumerator NextScene()
    {
        FindObjectOfType<AudioManager>().PlaySound("Teleport");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(5.13f);
        SceneManager.LoadScene(0);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
