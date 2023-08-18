using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    int TriggerTransitionHash = Animator.StringToHash("TriggerTransition");
    int TriggerUnTransitionHash = Animator.StringToHash("TriggerUnTransition");


    [SerializeField] Animator animator;
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
        animator.SetTrigger(TriggerTransitionHash);
        StartCoroutine(NextScene());
    }

    public void TriggerOpening()
    {
        animator.SetTrigger(TriggerUnTransitionHash);
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
