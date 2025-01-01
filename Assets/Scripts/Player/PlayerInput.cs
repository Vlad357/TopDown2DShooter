using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions _input;
    private IControllable _controllable;

    private void Awake()
    {
        _input = new InputSystem_Actions();
    }
    private void Start()
    {
        _controllable = GetComponent<IControllable>();

        if(_controllable == null)
        {
            new Exception($"Controllable not found. GameObject: {gameObject.name}");
        }
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Attack.performed += _ => _controllable.Shoot();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Attack.performed -= _ => _controllable.Shoot();
    }

    private void Update()
    {
        ReadMoveValue();
        ReadLookAtValue();
    }

    private void ReadLookAtValue()
    {
        var lookDirection = _input.Player.LookAtPoint.ReadValue<Vector2>();
        _controllable.LookAt(lookDirection);
    }

    private void ReadMoveValue()
    {
        var moveDirection = _input.Player.Move.ReadValue<Vector2>();
        _controllable.Move(moveDirection);
    }
}
