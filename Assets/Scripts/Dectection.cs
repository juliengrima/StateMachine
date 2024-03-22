using UnityEngine;
using UnityEngine.Events;

public class Dectection : MonoBehaviour
{
    #region Champs
    [SerializeField] Transform _playerTransform;
    [SerializeField] EntityMove _entityMove;
    [SerializeField] Waypoints _waypoints;
    [SerializeField] EnemyState _enemy;
    [SerializeField] float _checkSpeed;
    [SerializeField] UnityEvent _OnPlayerEnter;

    bool collision;
    int _state;
    private bool _playerInSight = false;


    public bool Collision { get => collision; }
    public int State { get => _state; }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _checkSpeed = 2f;
        _state = 0;
    }

    private void Start()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.attachedRigidbody == null) return;
        if (collision.attachedRigidbody.gameObject.CompareTag("Player"))
        {
            if (_entityMove.IsCrouching == true)
            {
                _state = 1;
                Debug.Log("Surveillance Accrue !");
            }
            else if (_entityMove.IsCrouching == false)
            {
                _state = 2;
                Debug.Log("ATTENTION !");
            }
        }
        else
        {
            _state = 0;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.attachedRigidbody == null) return;
        if (collision.attachedRigidbody.gameObject.CompareTag("Player"))
        {
            if (_entityMove.IsCrouching == true)
            {
                _state = 1;
                _waypoints.Speed *= _checkSpeed;
                Debug.Log("Sortie Surveillance Accrue !");
            }
            else if (_entityMove.IsCrouching == false)
            {
                _state = 2;
                Debug.Log("sortie ATTENTION !");
            }
        }
    }
    #endregion
    #region Methods
    public void PlayerDetected()
    {
        if (_playerTransform == null)
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        Ray ray = new Ray(transform.position, _playerTransform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _playerInSight = hit.collider.CompareTag("Player");
        }
    }
    public void PlayerLost()
    {
        _playerInSight = false;
    }
    #endregion
    #region Coroutines

    #endregion
}
