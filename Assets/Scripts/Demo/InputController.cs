using SupportiveLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputController {
    public class InputController : MonoBehaviour {
        public PlayerInput _playerInput;
        public InputAction _waveAction;
        public Queue<InputPack> _inputCache;
        public float _threshold;

        public void OnRythmChange(InputAction.CallbackContext context)
        {

        }

        public void WavingStart(InputAction.CallbackContext context)
        {
            
        }
        public void WavingPerform(InputAction.CallbackContext context) {
            Debug.Log($"Performed {context.ReadValue<float>()}");
        }

        public void WavingCancel(InputAction.CallbackContext context)
        {
            Debug.Log($"cancel {context.ReadValue<float>()}");
        }

        // Use this for initialization
        void Awake()
        { 

            _playerInput = GetComponent<PlayerInput>();
            
            // remmove this line when it is not demo!!!
            _playerInput.SwitchCurrentActionMap("Wave");

            _waveAction = _playerInput.actions["Waving"];
        }

        private void OnEnable()
        {
            _waveAction.performed += WavingPerform;
            _waveAction.canceled += WavingCancel;
        }

        private void OnDisable()
        {
            Debug.Log("On disable");
            _waveAction.performed -= WavingPerform;
            _waveAction.canceled -= WavingCancel;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}