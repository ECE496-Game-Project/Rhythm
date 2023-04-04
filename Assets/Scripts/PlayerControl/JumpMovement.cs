using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpMovement : MonoBehaviour
{
    
    
    [Header("Reference Variable")]
    [SerializeField]
    private Rigidbody _player;

    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private GroundDetector _groundDetector;

    
    
    
    
    
    [Header("Jumping Parameter")]
    [SerializeField]
    private float _maximumHeight;

    [SerializeField]
    private float _jumpingGravity;

    [SerializeField]
    private float _fallingGravity;

    [Tooltip("The jump will be registered if the jump button is pressed" +
        "at most this fram before landing the ground")]
    [SerializeField]
    private int _airJumpTolerance;

    [Tooltip("The maximum falling velocity it can be")]
    [SerializeField]
    private float _terminalVelocity;





    [Tooltip("The delay the character start falling when character is not on the ground")]
    [SerializeField]
    private float _coyoteTime;
    private bool _coyoteEnter, _coyoteExit;

    private bool CoyoteEnter
    {
        get { return _coyoteEnter; }
        set 
        {
            if (_coyoteCoroutine != null)
            {
                StopCoroutine(_coyoteCoroutine);
            }


            _coyoteEnter = value;
            
            if (_coyoteEnter == true)
            {
                _coyoteCoroutine = StartCoroutine(_coyoteClock(_coyoteTime));
            }
        }
    }

    private bool CoyoteExit
    {
        get { return _coyoteExit; }
        set { _coyoteExit = value; }
    }

    private Coroutine _coyoteCoroutine;
    private IEnumerator _coyoteClock(float count)
    {
        yield return new WaitForSeconds(count);
        _coyoteExit = true;
    }
    




    [Header("current state")]
    private float _speed;
    private float _acceleration;
    private float _height;

    
    
    
    [Header("Jump Input Management")]
    private bool _jumpBuffer;
    private Coroutine _airJumpToleranceClock;

    private bool JumpBuffer
    {
        get { return _jumpBuffer; }
        set
        {
            if (_airJumpToleranceClock != null)
            {
                StopCoroutine(_airJumpToleranceClock);
            }

            _jumpBuffer = value;

            if (_jumpBuffer == true)
            {
                _airJumpToleranceClock = StartCoroutine(AirJumpCountDown(_airJumpTolerance));
            }
            

        }
    }

    private IEnumerator AirJumpCountDown(int count )
    {
        int clock = (count <= 0) ? 1: count;

        while (clock > 0)
        {
            clock--;
            yield return null;
        }
        _jumpBuffer = false;

    }


    [Tooltip("The initial speed the character when it jump")]
    private float InitialSpeed {
        get { return Mathf.Sqrt(2 * Mathf.Abs(_jumpingGravity) * _maximumHeight); }  
    }


    [Header("Jumping State")]
    [SerializeField]
    private EJumpingState _jumpingState;
    enum EJumpingState
    {
        Standing,
        GoingUp,
        GoingDown
    }

     

    [Header("Input Sysetem")]
    InputAction _jumpAction;


    private void Awake()
    {
        _jumpAction = _playerInput.actions["Jump"];
    }

    private void OnEnable()
    {
        if (_jumpAction == null)
        {
            Debug.LogError("Can not find Jump action");
            return;
        }

        _jumpAction.performed += OnJumpPressed;
    }

    private void OnDisable()
    {
        if (_jumpAction == null)
        {
            Debug.LogError("Can not find Jump action");
            return;
        }

        _jumpAction.performed -= OnJumpPressed;
    }

    // Start is called before the first frame update
    void Start()
    {

        _jumpBuffer = false;
        _coyoteEnter = false;
        _coyoteExit = false;
        _speed = 0f;
        _acceleration = 0f;
        _height = 0f;
        _jumpingState = EJumpingState.Standing;

        
    }


    private void OnJumpPressed(InputAction.CallbackContext context)
    {
        JumpBuffer = true;
        
    }



    // Update is called once per frame
    void Update()
    {
        _speed += _acceleration * Time.deltaTime;

        if (_speed < 0 && -_speed >= _terminalVelocity) _speed = -_terminalVelocity;

        //float 
        float deltaHeight = _speed * Time.deltaTime;

        switch (_jumpingState)
        {
            case EJumpingState.Standing:
                if (!_groundDetector.OnGround)
                {
                    if (CoyoteEnter == false)
                    {
                        CoyoteEnter = true;
                    }
                    else if (CoyoteEnter == true && CoyoteExit == true)
                    {
                        _jumpingState = EJumpingState.GoingDown;
                        _acceleration = -_fallingGravity;
                        CoyoteEnter = false;
                        CoyoteExit= false;
                    }

                     
                }
                
                if(JumpBuffer == true)
                {
                    CoyoteEnter = false;
                    CoyoteExit = false;

                    JumpBuffer = false;
                    _speed = InitialSpeed;
                    _acceleration = -_jumpingGravity;
                    _jumpingState = EJumpingState.GoingUp;
                }
                break;
            case EJumpingState.GoingUp:

                if (_speed <= 0)
                {
                    //deltaHeight = _maximumHeight - _height;
                    _height = _maximumHeight;
                    _acceleration  = -_fallingGravity;
                    _jumpingState = EJumpingState.GoingDown;
                }

                break;  
            case EJumpingState.GoingDown:
                if (_groundDetector.OnGround)
                {
                    _speed = 0f;
                    _acceleration = 0f;
                    //_height= 0f;
                    _jumpingState = EJumpingState.Standing;
                }
                break;
        }
        
        
        transform.position += transform.up * deltaHeight;
    }
}
