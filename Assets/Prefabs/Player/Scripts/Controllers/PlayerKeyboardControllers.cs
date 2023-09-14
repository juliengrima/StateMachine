using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeayboardControllers : MonoBehaviour
{
    #region Champs
    [Header("Components")]
    [SerializeField] Rigidbody _rb;
    //[SerializeField] Grounded _grounded;
    [Header("Fieds")]
    [SerializeField] float _speed;
    [SerializeField] float _jumpHeight;
    //Privates Components
    //Privates Fields
    Transform _camera;
    Vector2 _direction;
    //Vector3 forwardBackward,straff;
    bool _isCrouchIsPressed, _isJumpIsPressed;
    //Setter - Getter
    //public Vector2 Direction { get => _direction; }
    public Rigidbody Rb { get => _rb; }

    //private int selectedWeaponIndex = 0; // Index de l'arme sélectionnée
    #endregion
    #region Unity LifeCycle
    private void Reset()
    {
        _speed = 1000f;
        _jumpHeight = 500f;
    }
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCrouchIsPressed)
        {
            
        }
        //Sera deplacé dans Interaction
        if (_fireIsPressed)
        {
            
        }
    }
    #endregion
    #region Methods
    void FixedUpdate ()
    {
        //Move camera on X / Y
        Vector3 camera = new Vector3(_camera.forward.x, 0f, _camera.forward.z);
        // use the camera's X direction to the player
        Vector3 forwardBackward = camera * _direction.y;
        forwardBackward.y = 0f;
        Vector3 straff = _camera.transform.right * _direction.x;
        Vector3 move = forwardBackward + straff;

        // PLayer look on the camera direction
        if (move != Vector3.zero)
        {
            //
            gameObject.transform.forward = camera;
        }

        Rb.velocity = move * _speed * Time.deltaTime;
    }
    public void Move(InputActionReference _move)
    {
        _direction = _move.action.ReadValue<Vector2>();   
    }
    public void Jump(InputActionReference _jump)
    {
        _isJumpIsPressed = _jump.action.WasPerformedThisFrame();
        //Debug.Log("Add Force to Rb Player");
        Rb.AddForce(Vector2.up * _jumpHeight);
    }
    public void Crouch(InputActionReference _crouch)
    {
        _isCrouchIsPressed = _crouch.action.WasPressedThisFrame();
    }

    //deplacer dans interactions
    bool _fireIsPressed;
    public void Fire(InputActionReference _fire)
    {
        _isCrouchIsPressed = _fire.action.WasPressedThisFrame();
    }
    #endregion
    #region Coroutines
    #endregion
}