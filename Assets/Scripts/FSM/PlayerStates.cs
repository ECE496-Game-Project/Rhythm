using System.Collections;
using UnityEngine;
using FSM;

namespace HFSM.PlayerStates {
    public class PlayerController : MonoBehaviour {

        private StateMachine _controlFSM;

        [Header("FSM Input")]
        private bool _isAim;
        private bool _isLink;
        private bool _isRun;
        private Vector3 _direction;

        [Header("State Variable")]
        private float _curSpeed;

        //private float Speed {
        //    get {
        //        return _curSpeed;
        //    };
        //    set {
        //        value = _curSpeed;
        //        //_animator.setvalue()
        //    }
        //}

        [Header("Movement Setting")]
        public float _walkSpeed = 1f;
        public float _runSpeed = 4f;
        public float _lerpDuration = 2f;

        [Header("Reference Variable")]
        private Animator _animator;
        // playWalk
        // playRun
        // playIdle

        private void playerMotionTransform(float speedLimit, float timeElapse, float LerpDuration) {
            _curSpeed = Mathf.Lerp(_curSpeed, speedLimit, timeElapse/LerpDuration);
            this.transform.position += _curSpeed * _direction * Time.deltaTime;
        }
        
        private void motionFSMStart() {
            
            StateMachine normalFSM = new StateMachine();

            _animator.SetBool("Walk", true);


            normalFSM.AddState("Idle", new State(
                onEnter: (state) => { _animator.SetBool("PlayIdle", true); },
                onLogic: (state) => playerMotionTransform(0f, state.timer.Elapsed, _lerpDuration),
                onExit: (state) => { _animator.SetBool("PlayIdle", false); })
                );
            
            
            normalFSM.AddState("Walk", new State(
                onEnter: (state) => { _animator.SetBool("PlayWalk", true); },
                onLogic: (state) => playerMotionTransform(_walkSpeed, state.timer.Elapsed, _lerpDuration),
                onExit: (state) => { _animator.SetBool("PlayWalk", false); })
                );

            normalFSM.AddState("Run", new State(
                onEnter: (state) => { _animator.SetBool("PlayRun", true); },
                onLogic: (state) => playerMotionTransform(_runSpeed, state.timer.Elapsed, _lerpDuration),
                onExit: (state) => { _animator.SetBool("PlayRun", false); })
                );
            
            
            normalFSM.AddTwoWayTransition("Idle", "Walk", t => _direction.magnitude > 0.0f);
            normalFSM.AddTwoWayTransition("Walk", "Run", t => _direction.magnitude > 0.0f && _isRun);
        }

        private void Start() {
            _controlFSM = new StateMachine();

            StateMachine normalFSM = new StateMachine();
            _controlFSM.AddState("Normal", normalFSM);
            
            StateMachine linkFSM = new StateMachine();
            _controlFSM.AddState("Link", linkFSM);

            _controlFSM.AddTransition(new Transition(
            "Normal",
            "Link",
            (transaition) => _isAim && _isLink
            ));

            _controlFSM.AddTransition(new Transition(
            "Link",
            "Normal",
            (transition) => !_isLink
            ));
            
            _controlFSM.SetStartState("Normal");
            _controlFSM.Init();
        }


        private void Update() {
            _controlFSM.OnLogic();
        }
    }

}