using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.PlayerStates;
public class PlayerController : MonoBehaviour, IAgent
{
    public bool isAim;
    public bool isLink;
    public Vector2 isMove;

    StateMachine m_StateMachine;
    // Start is called before the first frame update
    void Start()
    {
        m_StateMachine = new StateMachine(this);
        //m_StateMachine.AddStates([new IdleState(), new MoveState()]);
        m_StateMachine.AddState(new IdleState(this));
        //m_StateMachine.AddState(new MoveState());
        m_StateMachine.SetDefault((int)ETransformState.Idle);


    }

    public void idleHandler() {

    }

    // Update is called once per frame
    void Update()
    {
        m_StateMachine.Update();
        switch ((ETransformState)m_StateMachine.m_CurrState.m_StateType) {
            case ETransformState.Idle:
                break;
            case ETransformState.IdleAim:
                break;
            case ETransformState.Move:
                break;
            case ETransformState.MoveAim:
                break;
            case ETransformState.Link:
                break;
        }
    }
}
