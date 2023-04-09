using SupportiveLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class InputController : MonoBehaviour {
    [HideInInspector]
    public PlayerInput _playerInput;
    [HideInInspector]
    public InputAction _waveAction;
    public Queue<InputPack> _inputCache;

    private InputPack _currentIP;
    public float _threshold;

    public UnityEvent OnRythmChange;

    //public bool _trigger = false;

    public void OnRythmButtonPressed(InputAction.CallbackContext context)
    {
        OnRythmChange?.Invoke();
    }

    public void WavingStart(InputAction.CallbackContext context)
    {
        _currentIP = new InputPack(context.ReadValue<float>(), Time.time);
        _inputCache.Enqueue(_currentIP);
        foreach(var pack in _inputCache)
        {
            Debug.Log(pack.ToString());
        }
            
    }

    public void WavingPerform(InputAction.CallbackContext context) {

        //nothing
    }

    public void WavingCancel(InputAction.CallbackContext context)
    {
        _currentIP._EndTime = Time.time;
        foreach (var pack in _inputCache)
        {
            Debug.Log(pack.ToString());
        }
    }

    public void ClearCache()
    {
        while(_inputCache.Count != 0 && _inputCache.Peek()._EndTime != -1f)
        {
            _inputCache.Dequeue();
        }
        foreach (var pack in _inputCache)
        {
            Debug.Log(pack.ToString());
        }
    }

    public Queue<InputPack> GetICCCache()
    {
        return _inputCache;
    }

    // Use this for initialization
    void Awake()
    { 

        _playerInput = GetComponent<PlayerInput>();
            
        // remove this line when it is not demo!!!
        _playerInput.SwitchCurrentActionMap("Wave");

        _waveAction = _playerInput.actions["Waving"];
        _inputCache = new Queue<InputPack>();
    }

    private void OnEnable()
    {
        _waveAction.started += WavingStart;
        _waveAction.performed += WavingPerform;
        _waveAction.canceled += WavingCancel;
            
    }

    private void OnDisable()
    {
        _waveAction.started -= WavingStart;
        _waveAction.performed -= WavingPerform;
        _waveAction.canceled -= WavingCancel;
    }

    //private void Update()
    //{
    //    if (_trigger)
    //    {
    //        _trigger = false;
    //        ClearCache();
    //    }
    //}
}
