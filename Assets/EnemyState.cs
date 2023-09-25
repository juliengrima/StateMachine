using UnityEngine;

public class EnemyState : MonoBehaviour
{
    #region Champs
    [SerializeField] Transform _player;
    EnemyStates _currentState;
    [SerializeField] Waypoints _wayPoint;
    [SerializeField] Dectection _collision;
    [SerializeField] CoroutinesStates _coroutine;
    [SerializeField] float _wait;
    [SerializeField] float m_stopChaseRange = 8f;

    [SerializeField] Color _patrol;
    [SerializeField] Color _chase;
    [SerializeField] Color _reset;

    [SerializeField] Light _light;
    #endregion
    #region Enumerator
    public enum EnemyStates
    {
        PATROL,
        CHASE,
        RESET,
        State4,
        State5,
        State6,
        State7
    }
    #endregion
    #region Default Informations
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    
    void Awake()
    {

    }
    void Start()
    {
        OnStateEnter();
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
    #region StatesMachine
    void OnStateEnter()
    {
        switch (_currentState)
        {
            case EnemyStates.PATROL:
                _wayPoint.OnStateEnter();
                _light.color = _patrol; // Par exemple, changez la couleur en rouge
                break;
            case EnemyStates.CHASE:
                _light.color = _chase;
                break;
            case EnemyStates.RESET:
                _light.color = _reset;
                break;
            case EnemyStates.State4:
               
                break;
            case EnemyStates.State5:
               
                break;
            case EnemyStates.State6:
               
                break;
            case EnemyStates.State7:
                
                break;
            default:
                break;
        }
    }
    void OnStateUpdate()
    {
        switch (_currentState)
        {
            case EnemyStates.PATROL: //Base statement
                int _state = _collision.State;
                if (_state == 1)
                {
                    TransitionToState(EnemyStates.RESET);

                }
                else if (_state == 2)
                {
                    TransitionToState(EnemyStates.CHASE);
                }
                else
                {
                    return;
                }
                break;
            case EnemyStates.CHASE: // State Start to move and make interactions
                _wayPoint.Chase(_player);
                StartCoroutine(_coroutine.RestartGame());
                if (Vector3.Distance(_player.position, _wayPoint.GetCurrentWaypoint()) > m_stopChaseRange)
                {
                    StopCoroutine(_coroutine.RestartGame());
                    TransitionToState(EnemyStates.RESET);
                }
                break;
            case EnemyStates.RESET:
                StartCoroutine(_coroutine.CheckPlayer(_wait, _light, _reset));
                StopCoroutine(_coroutine.CheckPlayer(_wait, _light, _reset));
                TransitionToState(EnemyStates.PATROL);
                break;
            case EnemyStates.State4:
                break;
            case EnemyStates.State6:    
                break;
            case EnemyStates.State7:

                break;
            default:
                break;
        }
    }

    void OnStateExit()
    {
        switch (_currentState)
        {
            case EnemyStates.PATROL:
              
                break;
            case EnemyStates.CHASE:
               
                break;
            case EnemyStates.RESET:
                
                break;
            case EnemyStates.State4:
                break;
            case EnemyStates.State5:
                
                break;
            case EnemyStates.State6:
               
                break;
            case EnemyStates.State7:

                break;
            default:
                break;
        }
    }
    public void TransitionToState(EnemyStates nextState)
    {
        OnStateExit();
        _currentState = nextState;
        OnStateEnter();
    }
    #endregion
    #region Coroutines
    #endregion
}
