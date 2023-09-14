using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityJump : MonoBehaviour
{
    #region Champs
    [Header("Components")]
    [SerializeField] Rigidbody _rb;
    [Header("Fieds")]
    [SerializeField] float _jumpHeight;
    //Private Fields
    bool _isCrouchIsPressed, _isJumpIsPressed;
    //Setter - Getter
    public Rigidbody Rb { get => _rb; }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _jumpHeight = 500f;
    }
    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region Methods
    void FixedUpdate ()
    {
        Rb.AddForce(Vector2.up * _jumpHeight);
    }
    public void Jump(InputActionReference _jump)
    {
        _isJumpIsPressed = _jump.action.WasPerformedThisFrame();
        //Debug.Log("Add Force to Rb Player");    
    }
    #endregion
    #region Coroutines
    #endregion
}
