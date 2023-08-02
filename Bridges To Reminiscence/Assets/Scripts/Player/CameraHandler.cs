using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook _freeLookCamera;
    [SerializeField] CinemachineVirtualCamera _closeUpDialogueCamera;

    [SerializeField] CinemachineTargetGroup _targetGroup;

    List<CinemachineVirtualCameraBase> Cameras = new List<CinemachineVirtualCameraBase>();


    const float cameraWeight = 3f;
    const float cameraRadius = 2f;
    const int activatedCameraPriority = 5;
    const int resetCameraPriority = 1;

    private void Start()
    {
        Cameras.Add(_freeLookCamera);
        Cameras.Add(_closeUpDialogueCamera);

        ResetCameraPriority();
        TriggerNormalFreeLookCamera();
    }


    public void AddTargetGroup(Transform obj)
    {
        _targetGroup.AddMember(obj, cameraWeight, cameraRadius);
    }

    public void RemoveTargetGroup(Transform obj)
    {
        _targetGroup.RemoveMember(obj);
    }

    public void TriggerInteractCamera()
    {
        ResetCameraPriority();

        _closeUpDialogueCamera.Priority = activatedCameraPriority;
    }

    public void TriggerNormalFreeLookCamera()
    {
        ResetCameraPriority();

        _freeLookCamera.Priority = activatedCameraPriority;
    }

    private void ResetCameraPriority()
    {
        foreach(var c in Cameras)
        {
            c.Priority = resetCameraPriority;
        }
    }


}
