using System.Collections;
using UnityEngine;
using FSM;

namespace HFSM.PlayerStates {

    public class IdleState: State
    {
        PlayerController _controller;
        public IdleState(PlayerController controller):base(needsExitTime: false) 
        { 
            _controller = controller;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLogic()
        {
            base.OnLogic();
            _controller.MotionTransform(0f, timer.Elapsed, _controller._lerpDuration);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    public class WalkState : State
    {
        PlayerController _controller;
        public WalkState(PlayerController controller) : base(needsExitTime: false)
        {
            _controller = controller;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLogic()
        {
            base.OnLogic();
            _controller.MotionTransform(_controller._walkSpeed, timer.Elapsed, _controller._lerpDuration);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    public class AimIdleState : IdleState
    {
        public AimIdleState(PlayerController controller) : base(controller)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLogic()
        {
            base.OnLogic();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    public class AimWalkState : IdleState
    {
        public AimWalkState(PlayerController controller) : base(controller)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLogic()
        {
            base.OnLogic();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    public class PlayerController : MonoBehaviour {

        private StateMachine _controlFSM;
        private PlayerFSMInput _FSMInput;

        // Move to PlayerFSMInput
        [Header("Motion Input Map")]
        public bool _isAim = false;
        public bool _isConnect = false;
        public Vector3 _move = Vector3.zero;
        public Vector3 _switchTarget = Vector3.zero;
        // Move to PlayerFSMInput END

        [Tooltip("Player Current Speed")]
        public float _curSpeed = 0f;

        [Header("Movement Setting")]
        public float _walkSpeed = 1f;
        public float _lerpDuration = 2f;

        [Header("Reference Variable")]
        [SerializeField]
        private Animator _animator;

        #region Motion+Aim FSM

        private int MotionFSMSwitchCalc() {

            return 0;
        }

        public void MotionTransform(float speedLimit, float timeElapse, float LerpDuration) {
            _curSpeed = Mathf.Lerp(_curSpeed, speedLimit, timeElapse/LerpDuration);
            this.transform.position += _curSpeed * _move * Time.deltaTime;
        }
        

        private void updateAimableList() {
            // Get the current camera
            //Camera cam = Camera.current;

            // Get the tag of objects to retrieve
            //string tagToRetrieve = "MyTag";

            // Get all the objects with the specified tag that are currently visible on the screen
            //GameObject[] visibleObjects = GameObject.FindGameObjectsWithTag(tagToRetrieve).Where(obj => cam.IsObjectVisible(obj.transform)).ToArray();
        }

        private StateMachine MotionFSMInitalize() {

            StateMachine motionFSM = new StateMachine();

            State IdleState = new State(
                onEnter: (state) => {
                    //_animator.SetBool("PlayIdle", true);
                },
                onLogic: (state) => MotionTransform(0f, state.timer.Elapsed, _lerpDuration),
                onExit: (state) => {
                    //_animator.SetBool("PlayIdle", false);
                }
            );

            State WalkState = new State(
                onEnter: (state) => { 
                    //_animator.SetBool("PlayWalk", true); 
                },
                onLogic: (state) => MotionTransform(_walkSpeed, state.timer.Elapsed, _lerpDuration),
                onExit: (state) => { 
                    //_animator.SetBool("PlayWalk", false); 
                }
            );

            
            motionFSM.AddState("Idle", IdleState);
            motionFSM.AddState("Walk", WalkState);

            motionFSM.AddTwoWayTransition("Idle", "Walk", t => _move.magnitude > 0.0f);

            motionFSM.SetStartState("Idle");

            return motionFSM;
        }
#endregion

        private StateMachine LinkFSMInitalize() {
            StateMachine linkFSM = new StateMachine();

            State IdleState = new State(
                onEnter: (state) => {
                    //_animator.SetBool("PlayIdle", true);
                },
                onLogic: (state) => MotionTransform(0f, state.timer.Elapsed, _lerpDuration),
                onExit: (state) => {
                    //_animator.SetBool("PlayIdle", false);
                }
            );

            return linkFSM;
        }

        private void Start() {
            _controlFSM = new StateMachine();

            _controlFSM.AddState("Normal", MotionFSMInitalize());
            _controlFSM.AddState("Link", LinkFSMInitalize());

            _controlFSM.AddTwoWayTransition("Normal", "Link", t => _isAim && _isConnect);
            
            _controlFSM.SetStartState("Normal");
            _controlFSM.Init();


            var unAimFSM = new StateMachine();
            unAimFSM.AddState("erwr", MotionFSMInitalize());
            var aimFSM = new StateMachine();

        }

        private void Update() {
            _controlFSM.OnLogic();
        }
    }

}