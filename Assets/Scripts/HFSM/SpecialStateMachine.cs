using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialStateMachine: StateMachine
{
    Action _onEnter;
    Action _onLogic;
    Action _onExit;

    public SpecialStateMachine(Action onEnter = null, Action onLogic = null, Action onExit = null, bool needsExitTime = false) : base(needsExitTime: needsExitTime)
    {
        _onEnter = onEnter;
        _onLogic = onLogic;
        _onExit = onExit;
    }

    public override void OnEnter()
    {
        _onEnter?.Invoke();
        base.OnEnter();
    }

    public override void OnLogic()
    {
        _onLogic?.Invoke();
        base.OnLogic();
    }

    public override void OnExit()
    {
        
        base.OnExit();
        _onExit?.Invoke();
    }
}
