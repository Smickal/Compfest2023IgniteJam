using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Projector : TriggerActivateGameObject
{
    const float PresentTime = 3f;

    [SerializeField] Sprite[] images;
    [SerializeField] bool isLooping;
    [SerializeField] Image _presentScreen;
    [SerializeField] Light _projektorLight;
    [Space(5)]
    [SerializeField] GameObject _screenOff;
    [SerializeField] GameObject _screenOn;

    bool isTriggered = false;
    bool isActivated = false;

    private void Start()
    {
        _screenOff.SetActive(true);
        _screenOn.SetActive(false);
        _projektorLight.gameObject.SetActive(false);
    }

    public override void RegisterTrigger()
    {
        interactedItem.OnTriggerAction += ActivateProjector;
    }

    public void ActivateProjector()
    {
        isActivated = true;

        _screenOff.SetActive(false);
        _screenOn.SetActive(true);
        _projektorLight.gameObject.SetActive(true);

        interactedItem.OnTriggerAction -= ActivateProjector;
    }

    private void Update()
    {
        if(isLooping && !isTriggered && isActivated)
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
