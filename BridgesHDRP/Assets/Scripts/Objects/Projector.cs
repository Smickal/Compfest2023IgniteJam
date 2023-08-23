using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Projector : MonoBehaviour
{
    const float PresentTime = 3f;

    [SerializeField] Sprite[] images;
    [SerializeField] bool isLooping;
    [SerializeField] Image _presentScreen;

    bool isTriggered = false;



    private void Update()
    {
        if(isLooping && !isTriggered)
        {
            StartCoroutine(TriggerProjectorImage());
        }
    }

    IEnumerator TriggerProjectorImage()
    {
        isTriggered = true;

        foreach (var image in images)
        {
            _presentScreen.sprite = image;
            yield return new WaitForSeconds(PresentTime);
        }

        isTriggered = false;
    }
}
