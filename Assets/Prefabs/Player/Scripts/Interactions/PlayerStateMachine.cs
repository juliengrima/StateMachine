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
    [Header("Player_Components")]
    [SerializeField] EntityMove _entityMove;
    [SerializeField] EntityJump _entityJump;
    [SerializeField] EntityCrouch _entityCrouch;
    //[SerializeField] CoroutinesStates _coroutines;
    [SerializeField] Grounded _Grounded;
    //[SerializeField] Interaction _interaction;
    [Header("Player_Animations")]
    [SerializeField] Animator _animator;
    //[Header("Player_Audios")]
    //[SerializeField] AudioSource _source;
    //[SerializeField] AudioClip _clip;
    //Private Fields
    CoroutinesStates _coroutines;
    Coroutine _fallCoroutine;
    //Private Components
    int _fallWait;

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
        //_animator = GetComponent<Animator>();
        _coroutines = GetComponent<CoroutinesStates>();
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
                _animator.SetBool("IDLE", true);
                break;
            case PlayerState.WALK:
                _animator.GetFloat(0);
                break;
            case PlayerState.RUN:
                _animator.GetFloat(6);
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
            case PlayerState.IDLE:
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
                    else if (_crouch.action.WasPerformedThisFrame())
                    {
                        //Debug.Log("Is crouching");
                        TransitionToState(PlayerState.CROUCH);
                    }
                    
                    //Interactions will come
                    //Fire will come    
                }
                else
                {
                    //Use coroutine to Wait before
                    _fallCoroutine = StartCoroutine(_coroutines.FallCoroutine(this));
                    //TransitionToState(PlayerState.FALL);
                }   
                break;
            case PlayerState.WALK:
                if (_Grounded.IsGrounded == true)
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
                    //Interactions will come
                    //Fire will come
                }
                else
                {
                    _fallCoroutine = StartCoroutine(_coroutines.FallCoroutine(this));
                }
                break;
            case PlayerState.RUN:
                if (_Grounded.IsGrounded == true)
                {
                    if (_jump.action.WasPerformedThisFrame())
                    {
                        TransitionToState(PlayerState.JUMP);
                    }
                    else if (_crouch.action.WasPerformedThisFrame())
                    {
                        TransitionToState(PlayerState.CROUCH);
                    }
                  
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
                    //_entityJump.Jump(_jump);
                }
                else
                {
                    _fallCoroutine = StartCoroutine(_coroutines.FallCoroutine(this));
                }
                break;
            case PlayerState.CROUCH:
                if (_Grounded.IsGrounded)
                {
                    if (_crouch.action.WasPerformedThisFrame())
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                    //Fire Will come
                }
                else
                {
                    _fallCoroutine = StartCoroutine(_coroutines.FallCoroutine(this));
                }
                break;
            case PlayerState.FALL:
                //var falling = _entityMove.Rb.velocity.y;
                //if (falling < 3000)
                //{
                //    TransitionToState(PlayerState.IDLE);
                //}
                //else
                //{
                //    TransitionToState(PlayerState.DEATH);
                //}
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
                _animator.SetBool("IDLE", false);
                break;
            case PlayerState.WALK:
                _animator.SetFloat("WALK", 0.01f);
                break;
            case PlayerState.RUN:
                _animator.SetFloat("RUN", 5f);
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
