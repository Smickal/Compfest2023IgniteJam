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
    [SerializeField] float defaultCameraSpeed = 200f;

    List<CinemachineVirtualCameraBase> Cameras = new List<CinemachineVirtualCameraBase>();

    Transform currTarget = null;

    Vector3 defaultCameraPos;
    Quaternion defaultCameraDir;


    private void Start()
    {
        Cameras.Add(_freeLookCamera);
        Cameras.Add(_closeUpDialogueCamera);

        ResetCameraPriority();
        TriggerNormalFreeLookCamera();

        defaultCameraPos = _closeUpDialogueCamera.transform.localPosition;
        defaultCameraDir = _closeUpDialogueCamera.transform.rotation;    
    }


    private void AddTargetGroup(Transform obj)
    {
        _targetGroup.AddMember(obj, cameraWeight, cameraRadius);
    }

    private void RemoveTargetGroup(Transform obj)
    {
        _targetGroup.RemoveMember(obj);
    }

    public void TriggerInteractCamera(Transform target)
    {
        DisableFreeLookCamera();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        ResetCameraPriority();

        currTarget = target;

        AddTargetGroup(currTarget);

        _closeUpDialogueCamera.Priority = activatedCameraPriority;
    }

    public void TriggerNormalFreeLookCamera()
    {
        EnableFreeLookCamera();

        Cursor.visible = false;    
        Cursor.lockState = CursorLockMode.Locked;
        ResetCameraPriority();


        RemoveTargetGroup(currTarget);
        _freeLookCamera.Priority = activatedCameraPriority;
    }

    private void ResetCameraPriority()
    {
        foreach(var c in Cameras)
        {
            c.Priority = resetCameraPriority;
        }
    }

    public void MoveInpectCameraToCustomLoc(Vector3 pos)
    {
        _closeUpDialogueCamera.transform.localPosition = pos;
    }


    public void ResetInspectCameraPosition()
    {
        _closeUpDialogueCamera.transform.localPosition = defaultCameraPos;
        _closeUpDialogueCamera.transform.rotation = defaultCameraDir;
    }


    public void DisableFreeLookCamera()
    {
        _freeLookCamera.m_XAxis.m_MaxSpeed = 0;
    }

    public void EnableFreeLookCamera()
    {
        StartCoroutine(EnableCamera());      
    }

    IEnumerator EnableCamera()
    {
        yield return new WaitForSeconds(1.9f);
        _freeLookCamera.m_XAxis.m_MaxSpeed = defaultCameraSpeed;
    }
}
