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

        }

        public void WavingCancel(InputAction.CallbackContext context)
        {

        }

        // Use this for initialization
        void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _waveAction = _playerInput.actions["Waving"];

            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}