using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    const float cameraWeight = 3f;
    const float cameraRadius = 2f;
    const int activatedCameraPriority = 50;
    const int resetCameraPriority = 40;


    [SerializeField] CinemachineFreeLook _freeLookCamera;
    [SerializeField] CinemachineVirtualCamera _closeUpDialogueCamera;

    [SerializeField] CinemachineTargetGroup _targetGroup;

    List<CinemachineVirtualCameraBase> Cameras = new List<CinemachineVirtualCameraBase>();



    Vector3 defaultCameraPos;
    Quaternion defaultCameraDir;

    private void Start()
    {
        Cameras.Add(_freeLookCamera);
        Cameras.Add(_closeUpDialogueCamera);

        ResetCameraPriority();
        TriggerNormalFreeLookCamera();

        defaultCameraPos = _closeUpDialogueCamera.transform.position;
        defaultCameraDir = _closeUpDialogueCamera.transform.rotation;
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
        Cursor.visible = true;
        ResetCameraPriority();

        _closeUpDialogueCamera.Priority = activatedCameraPriority;
    }

    public void TriggerNormalFreeLookCamera()
    {
        Cursor.visible = false;    
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

    public void MoveInpectCameraToCustomLoc(Vector3 pos, Vector3 cameraRot)
    {
        _closeUpDialogueCamera.transform.localPosition = pos;
        _closeUpDialogueCamera.transform.rotation = Quaternion.Euler(cameraRot);
    }


    public void ResetInspectCameraPosition()
    {
        _closeUpDialogueCamera.transform.localPosition = defaultCameraPos;
        _closeUpDialogueCamera.transform.rotation = defaultCameraDir;
    }
}
