using System.Collections;
using UnityEngine;

namespace FSM.PlayerStates {
    //enum ETransformState {
    //    Idle, Move, IdleAim, MoveAim, Link
    //}

    //public class PlayerState : State {
    //    protected PlayerController _playerController;

    //    public PlayerState(PlayerController playerController){
    //        this._playerController = playerController;
    //    }
    //}

    //public class IdleState : PlayerState {
    //    public IdleState(PlayerController playerController) : base(playerController) {
    //        m_StateType = (int) ETransformState.Idle;
    //    }

    //    public override State Execute() {
    //        _playerController.idleHandler();

    //        ETransformState nextEState = ETransformState.Idle;

    //        if (_playerController.isAim && _playerController.isMove != Vector2.zero)
    //            nextEState = ETransformState.MoveAim;
            
    //        else if(_playerController.isAim)
    //            nextEState = ETransformState.IdleAim;
            
    //        else if (_playerController.isMove != Vector2.zero)
    //            nextEState = ETransformState.Move;
            
    //        else if (_playerController.isLink)
    //            Debug.Log("Idlestate: despert click link");

    //        // Idle execution
    //        return m_StateMachine.Transition((int)nextEState);
    //    }
    //}

    //public class IdleAimState : PlayerState {
    //    public IdleAimState(PlayerController playerController) : base(playerController) {
    //        m_StateType = (int) ETransformState.IdleAim;
    //    }
    //    public override State Execute() {
    //        ETransformState nextEState = ETransformState.IdleAim;

    //        if (_playerController.isAim == true && _playerController.isLink == true)
    //            nextEState = ETransformState.Link;
    //        else if (_playerController.isMove != Vector2.zero)
    //            nextEState = ETransformState.MoveAim;
    //        else if (_playerController.isAim == false)
    //            nextEState = ETransformState.Idle;
            
    //        // Idle execution
    //        return m_StateMachine.Transition((int)nextEState);
    //    }
    //}

    //public class MoveState : PlayerState
    //{
    //    public MoveState(PlayerController playerController) : base(playerController)
    //    {
    //        m_StateType = (int) ETransformState.Move;
    //    }

    //    public override State Execute()
    //    {
    //        int nextState = (int)m_StateType;

    //        if (_playerController.isLink){
    //            Debug.Log("Idlestate: despert click link");
    //        }
    //        else if (_playerController.isAim && _playerController.isMove == Vector2.zero) {
    //            nextState = (int)ETransformState.IdleAim;
    //        }
    //        else if (_playerController.isAim)
    //        {
    //            nextState = (int)ETransformState.MoveAim;
    //        }
    //        else if (_playerController.isMove == Vector2.zero)
    //        {
    //            nextState = (int)ETransformState.Idle;
    //        }
            
    //        // Idle execution
    //        return m_StateMachine.Transition(nextState);
    //    }
    //}

    //public class MoveAimState : PlayerState
    //{
    //    public MoveAimState(PlayerController playerController) : base(playerController)
    //    {
    //        m_StateType = (int) ETransformState.MoveAim;
    //    }

    //    public override State Execute()
    //    {
    //        ETransformState nextEState = ETransformState.MoveAim;

    //        if (_playerController.isLink)
    //        {
    //            nextEState = ETransformState.Link;
    //        }
    //        else if (!_playerController.isAim && _playerController.isMove == Vector2.zero)
    //        {
    //            nextEState = ETransformState.Idle;
    //        }
    //        else if (!_playerController.isAim)
    //        {
    //            nextEState = ETransformState.Move;
    //        }
    //        else if (_playerController.isMove == Vector2.zero)
    //        {
    //            nextEState = ETransformState.IdleAim;
    //        }
    //        // Idle execution
    //        return m_StateMachine.Transition((int) nextEState);
    //    }
    //}

    //public class LinkState : PlayerState {
    //    public LinkState(PlayerController playerController) : base(playerController) {
    //        m_StateType = (int) ETransformState.Link;
    //    }

    //    public override void Enter() {
    //        _playerController.isAim = false;
    //        _playerController.isMove = Vector2.zero;
    //    }

    //    public override State Execute() {
    //        ETransformState nextEState = ETransformState.Link;

    //        if (!_playerController.isLink) {
    //            nextEState = ETransformState.Idle;
    //        }
    //        // Idle execution
    //        return m_StateMachine.Transition((int) nextEState);
    //    }
    //}
}