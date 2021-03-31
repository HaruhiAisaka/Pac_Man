using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("Components")]
    PlayerMovement playerMovement;
    PlayerControls controls;
    Vector2 movementVector;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        controls = new PlayerControls();
        controls.Player.Movement.performed += ctx =>
            UpdateInputDirection(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled += ctx => SendButtonReleased();
    }

    void UpdateInputDirection(Vector2 value)
    {
        playerMovement.UpdateInputDirection(value);
    }

    void SendButtonReleased()
    {
        playerMovement.UpdateInputDirection(Vector2.zero);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
