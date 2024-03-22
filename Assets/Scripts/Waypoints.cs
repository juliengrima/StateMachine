using UnityEngine;
using UnityEngine.AI;

public class Waypoints : MonoBehaviour
{
    #region Champs
    [SerializeField] Transform[] _waypoint;
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] AgentMode _currentMode;
    [SerializeField] float _speed; // Vitesse de déplacement du GameObject

    private int _currentWaypoint = 0;
    bool _pong;

    public enum AgentMode
    {
        LOOP,
        NEGATIVE_LOOP,
        PING_PONG
    }
    public float Speed { get => _speed; set => _speed = value; }
    #endregion
    #region Unity LifeCycle
    private void Reset()
    {
        Speed = 2f;
        //_distance = 0.5f;
    }
    // Start is called before the first frame update
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void Start()
    {
        // Assurez-vous que le tableau des waypoints n'est pas vide
        if (_waypoint.Length == 0)
        {
            Debug.LogError("Aucun waypoint n'a été assigné.");
            enabled = false; // Désactiver le script pour éviter des erreurs
        }
        else
        {
            var mode = _currentMode;
        }   
    }
    // Update is called once per frame
    public void Update()
    {
        OnStateUpdate();
    }
    #endregion
    #region States
    public void OnStateEnter()
    {
        switch (_currentMode)
        {
            case AgentMode.LOOP:
                _navMeshAgent.SetDestination(_waypoint[_currentWaypoint].position);
                break;
            case AgentMode.NEGATIVE_LOOP:
                _navMeshAgent.SetDestination(_waypoint[_waypoint.Length -1].position);
                break;
            case AgentMode.PING_PONG:
                _navMeshAgent.SetDestination(_waypoint[_currentWaypoint].position);
                break;
            default:
                Debug.Log("Valeur du Waypoint Unknow");
                break;
        }
    }
    public void OnStateUpdate()
    {
        if (_navMeshAgent.remainingDistance >= 1f)
            return;

        switch (_currentMode)
        {
            case AgentMode.LOOP:
                Increment();
                break;
            case AgentMode.NEGATIVE_LOOP:
                Decrement();
                break;
            case AgentMode.PING_PONG:
                if (_pong)
                {
                    _currentWaypoint++;

                    if (_currentWaypoint > _waypoint.Length - 1)
                    {
                        _pong = false;
                    }

                    _navMeshAgent.SetDestination(_waypoint[_waypoint.Length - 1].position);
                }
                else
                {
                    _currentWaypoint--;

                    if (_currentWaypoint < 0)
                    {
                        _pong = true;
                    }

                    _navMeshAgent.SetDestination(_waypoint[_currentWaypoint].position);
                }
                break;
            default:
                break;
        }
    }
    void OnStateExit()
    {
        switch (_currentMode)
        {
            case AgentMode.LOOP:
                _navMeshAgent.SetDestination(_waypoint[_currentWaypoint].position);
                break;
            case AgentMode.NEGATIVE_LOOP:
                _navMeshAgent.SetDestination(_waypoint[_waypoint.Length - 1].position);
                break;
            case AgentMode.PING_PONG:
                break;
            default:
                break;
        }
    }

    public void TransitionToState(AgentMode nextState)
    {
        OnStateExit();
        _currentMode = nextState;
        OnStateEnter();
    }
    #endregion
    #region Methods
    private void Increment()
    {
        _currentWaypoint++;

        if (_currentWaypoint > _waypoint.Length - 1)
        {
            _currentWaypoint = 0;
        }

        _navMeshAgent.SetDestination(_waypoint[_currentWaypoint].position);
    }

    private void Decrement()
    {
        _currentWaypoint--;

        if (_currentWaypoint < 0)
        {
            _currentWaypoint = _waypoint.Length - 1;
        }

        _navMeshAgent.SetDestination(_waypoint[_currentWaypoint].position);
    }

    public void Chase(Transform target)
    {
        _navMeshAgent.SetDestination(target.position);
    }

    public Vector3 GetCurrentWaypoint()
    {
        return _waypoint[_currentWaypoint].position;
    }
    #endregion
}
