using UnityEngine;

public class PatrolCamera : MonoBehaviour
{
    #region Champs
    [Header("Detection Player")]
    [SerializeField] Transform _playerTarget;
    [Header("Patrol camera")]
    [Header("Components")]
    [SerializeField] Transform _startTarget;
    [SerializeField] Transform _endTarget;
    [SerializeField] Transform _camera;
    [Header("Fields values")]
    [SerializeField] float _turnSpeed;
    [SerializeField] float _range;
    [SerializeField] float _travel;

    float _chrono = 0f;
    private bool _ping = true;

    public enum CameraState
    {
        PATROL, PURSUIT, RESET
    }
    CameraState _currentState;
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _range = 15f;
        _turnSpeed = 2f;
        _travel = 3f;
    }
    void Awake()
    {
        
    }
    void Start()
    {
        transform.LookAt(_startTarget);
    }
    // Update is called once per frame
    void Update()
    {
        Patrol(); 
    }
    #endregion
    #region Methods
    void Patrol()
    {
        //Interpolation linéaire entre Position d"but et fin
        Vector3 lookAtPosition = Vector3.Lerp(_startTarget.position, _endTarget.position, _chrono / _travel);

        if (_ping)
        {
            _chrono += Time.deltaTime;
            // Si la valeur du chrono est sup�rieure � la dur�e du trajet c'est que le trajet est termin�, donc on fait demi-tour
            if (_chrono >= _travel)
            {
                _ping = false;
            }
        }
        else
        {
            _chrono -= Time.deltaTime;
            // Si chrono est plus petit que 0 c'est qu'on a fini de revenir
            if (_chrono < 0f)
            {
                _ping = true;
            }
        }
        transform.LookAt(lookAtPosition);
    }
    void LockOnTarget()
    {
        // DÃ©terminiation de la rotation de la tourrelle en fonction de la position de l'ennemi.
        Vector3 dir = _playerTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(_camera.rotation, lookRotation, Time.deltaTime * _turnSpeed).eulerAngles;
        _camera.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void OnTriggerEnter(Collider collision)
    {
        LockOnTarget();
    }
    private void OnTriggerExit(Collider collision)
    {
        Patrol();
    }
    #endregion
    #region Coroutines
    #endregion
}
