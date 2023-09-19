using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerStateMachine;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class PlayerStateMachine : MonoBehaviour
{
    #region Champs
    [Header("Player_Controllers")]
    [SerializeField] InputActionReference _move;
    [SerializeField] InputActionReference _jump;
    [SerializeField] InputActionReference _crouch;
    [Header("Player_Actions_Components")]
    [SerializeField] EntityMove _entityMove;
    [SerializeField] EntityJump _entityJump;
    [SerializeField] EntityCrouch _entityCrouch;
    [SerializeField] CoroutinesStates _coroutines;
    [Header("Player_interactions_Components")]
    [SerializeField] Grounded _Grounded;
    [SerializeField] Interactions _interactions;
    //[SerializeField] Interaction _interaction;
    [Header("Player_Animations")]
    [SerializeField] Animator _animator;
    //[Header("Player_Audios")]
    //[SerializeField] AudioSource _source;
    //[SerializeField] AudioClip _clip;
    //Private Fields
    Coroutine _fallCoroutine;
    Vector2 _dir;
    //Private Components
    int _fallWait;

    private void Reset()
    {
        _entityMove = transform.parent.GetComponentInChildren<EntityMove>();
        _entityJump = transform.parent.GetComponentInChildren<EntityJump>();
        _entityCrouch = transform.parent.GetComponentInChildren<EntityCrouch>();
        _Grounded = transform.parent.GetComponentInChildren<Grounded>();
        _interactions = transform.parent.GetComponentInChildren<Interactions>();
        _animator = transform.parent.GetComponentInChildren<Animator>();
        _coroutines = transform.parent.GetComponentInChildren<CoroutinesStates>();
    }

    //Enumerator
    public  enum PlayerState
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        CROUCH,
        FALL,
        DEATH
    }

    PlayerState _currentState = PlayerState.IDLE;

    public int FallWait { get => _fallWait; }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    void Awake()
    {
  
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OnStateUpdate();
    }
    #endregion
    #region Methods
    void FixedUpdate()
    {

    }
    void LateUpdate()
    {

    }
    #endregion
    #region Coroutines
    #endregion
    #region States
    void OnStateEnter()
    {
        switch (_currentState)
        {
            case PlayerState.IDLE:
                _entityMove.Move(_move);
                break;
            case PlayerState.WALK:
                break;
            case PlayerState.RUN:
                break;
            case PlayerState.JUMP:
                _animator.SetTrigger("JUMP");
                break;
            case PlayerState.CROUCH:
                _animator.SetBool("CRUNCH", true);
                break;
            case PlayerState.FALL:
                _animator.SetBool("FALL", true);
                break;
            case PlayerState.DEATH:
                _animator.SetBool("DEATH", true);
                break;
            default:
                break;
        }
    }
    void OnStateUpdate()
    {
        

        switch (_currentState)
        {
            case PlayerState.IDLE: //Base statement
                
                _animator.SetFloat("X", 0);
                _animator.SetFloat("X", 0);
                if (_Grounded.IsGrounded == true)
                {
                    //Debug.Log("Is Grounded");
                    if (_move.action.WasPerformedThisFrame())
                    {
                        //Debug.Log("Is Moving");
                        TransitionToState(PlayerState.WALK);
                    }
                    else if (_jump.action.WasPerformedThisFrame())
                    {
                        //Debug.Log("Is Jumping");
                        TransitionToState(PlayerState.JUMP);
                    }
                    
                    _interactions.Interations();
                    //Fire will come    
                }
                else
                {
                    //Use coroutine to Wait before
                    _fallCoroutine = StartCoroutine(_coroutines.FallCoroutine(this));
                    //TransitionToState(PlayerState.FALL);
                }   
                break;
            case PlayerState.WALK: // State Start to move and make interactions
                if (_Grounded.IsGrounded == true)
                {
                    _dir = _move.action.ReadValue<Vector2>();
                    _animator.SetFloat("X", _dir.x);
                    _animator.SetFloat("Y", _dir.y);
                    if (_dir.magnitude <= 0f)
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                    else if ( _dir.magnitude < 0.3f)
                    {
                        if (_jump.action.WasPerformedThisFrame())
                        {
                            TransitionToState(PlayerState.JUMP);
                        }
                        else if (_crouch.action.WasPerformedThisFrame())
                        {
                            TransitionToState(PlayerState.CROUCH);
                        }

                        _entityMove.Move(_move);
                        _interactions.Interations();
                        //Fire will come
                    }
                    else if (_dir.magnitude > 0.7f)
                    {
                        TransitionToState(PlayerState.RUN);
                    }
                }
                else
                {
                    _fallCoroutine = StartCoroutine(_coroutines.FallCoroutine(this));
                }
                break;
            case PlayerState.RUN:
                _dir = _move.action.ReadValue<Vector2>();
                _animator.SetFloat("X", _dir.x * 2);
                _animator.SetFloat("Y", _dir.y * 2);
                if (_Grounded.IsGrounded == true)
                {
                    var dir = _move.action.ReadValue<Vector2>();
                    if (dir.magnitude <= 0.01f)
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                    else if (dir.magnitude < 0.6f)
                    {
                        TransitionToState(PlayerState.WALK);
                    }

                    if (_jump.action.WasPerformedThisFrame())
                    {
                        TransitionToState(PlayerState.JUMP);
                    }

                    if (_crouch.action.WasPerformedThisFrame())
                    {
                        TransitionToState(PlayerState.CROUCH);
                    }
                    _entityMove.Move(_move);
                    //Fire will come
                }
                else
                {
                    TransitionToState(PlayerState.FALL);
                }
                break;
            case PlayerState.JUMP: 
                if (_Grounded.IsGrounded)
                {
                    if (_move.action.WasPerformedThisFrame())
                    {
                        TransitionToState(PlayerState.WALK);
                    }
                    else if (_jump.action.WasPerformedThisFrame())
                    {
                        TransitionToState(PlayerState.JUMP);
                    }
                    else if (_crouch.action.WasPerformedThisFrame())
                    {
                        TransitionToState(PlayerState.CROUCH);
                    }
                    else
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                    //Debug.Log(" Go to Jump() Method");
                    _entityJump.Jump(_jump);
                }
                else
                {
                    _fallCoroutine = StartCoroutine(_coroutines.FallCoroutine(this));
                }
                break;
            case PlayerState.CROUCH:
                if (_Grounded.IsGrounded)
                {
                    var dir = _move.action.ReadValue<Vector2>();
                    if (dir.magnitude <= 0.05f)
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                    
                    //Fire Will come
                    _entityMove.Crouch(_crouch);
                }
                else
                {
                    _fallCoroutine = StartCoroutine(_coroutines.FallCoroutine(this));
                }
                break;
            case PlayerState.FALL:
                var falling = _entityMove.Rb.velocity.y;
                if (falling < 1000)
                {
                    TransitionToState(PlayerState.IDLE);
                }
                else
                {
                    TransitionToState(PlayerState.DEATH);
                }
                break;
            case PlayerState.DEATH:

                break;
            default:
                break;
        }
    }

    void OnStateExit()
    {
        switch (_currentState)
        {
            case PlayerState.IDLE:
                _animator.SetFloat("Blend", 0.1f);
                break;
            case PlayerState.WALK:
                _animator.SetFloat("Blend", 0f);
                break;
            case PlayerState.RUN:
                _animator.SetFloat("Blend", 0.6f);
                break;
            case PlayerState.JUMP:
                break;
            case PlayerState.CROUCH:
                _animator.SetBool("CROUCH", false);
                break;
            case PlayerState.FALL:
                _animator.SetBool("FALL", false);
                break;
            case PlayerState.DEATH:

                break;
            default:
                break;
        }
    }
    public void TransitionToState(PlayerState nextState)
    {
        OnStateExit();
        _currentState = nextState;
        OnStateEnter();
    }
    #endregion
}
