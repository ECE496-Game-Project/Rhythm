using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerFSMInput : MonoBehaviour
{
    public PlayerInput _playerInput;

    [Header("Motion Input Map")]
    public InputAction _moveAction;
    public InputAction _aimAction;
    public InputAction _connectAction;
    public InputAction _switchTargetAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _aimAction = _playerInput.actions["Aim"];
        _connectAction = _playerInput.actions["Connect"];
        _switchTargetAction = _playerInput.actions["SwitchTarget"];
        
        _playerInput.SwitchCurrentActionMap("Link");
    }

    public void switchToInputMapping(/*Mapping Enum*/) {
        //_playerInput.SwitchCurrentActionMap();
    }
}
