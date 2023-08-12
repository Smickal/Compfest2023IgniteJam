using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputControls.IPlayerActions
{
    private InputControls controls;
    public Vector2 MovementValue {  get; private set; }

    public event Action OnInteractPressed;

    private void Start()
    {
        controls = new InputControls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }


    public void OnWalk(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnInteractPressed?.Invoke();
    }
}
