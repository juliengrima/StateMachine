using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityMove : MonoBehaviour
{
    #region Champs
    [Header("Components")]
    [SerializeField] GameObject _player;
    [SerializeField] Rigidbody _rb;
    //[SerializeField] Grounded _grounded;
    [Header("Fieds")]
    [SerializeField] float _speed;
    //Privates Components
    //Privates Fields
    Transform _camera;
    Vector2 _direction;
    //Setter - Getter
    //public Vector2 Direction { get => _direction; }
    public Rigidbody Rb { get => _rb; }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _speed = 1000f;
        _rb = transform.parent.GetComponentInChildren<Rigidbody>();
    }
   
    void Start()
    {
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    #endregion
    #region Methods
    void FixedUpdate ()
    {
        //Move camera on X / Z 
        Vector3 camera = new Vector3(_camera.forward.x, 0f, _camera.forward.z);
        // use the camera's X direction to the player
        Vector3 forwardBackward = camera * _direction.y;
        forwardBackward.y = 0f;
        Vector3 straff = _camera.transform.right * _direction.x;
        Vector3 move = forwardBackward + straff;

        // PLayer look on the camera direction
        if (move != Vector3.zero)
        {
            _player.transform.forward = camera;
        }
        //variable direction du vector2
        var dir = move * _speed * Time.deltaTime;
        // Velocity of vector2 with y velocity
        Rb.velocity = new Vector3(dir.x, Rb.velocity.y, dir.z);
    }
    public void Move(InputActionReference _move)
    {
        _direction = _move.action.ReadValue<Vector2>();
    }
    #endregion
    #region Coroutines
    #endregion
}
