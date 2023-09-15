using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityCrouch : MonoBehaviour
{
    #region Champs
    [Header("Components")]
    [SerializeField] Rigidbody _rb;
    //[Header("Fieds")]
    //Privates Components
    //Privates Fields
    bool _isCrouchIsPressed;
    //Setter - Getter
    public Rigidbody Rb { get => _rb; }
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
        
    }
    #endregion
    #region Methods
    void FixedUpdate ()
    {
        
    }
    public void Crouch(InputActionReference _crouch)
    {
        _isCrouchIsPressed = _crouch.action.WasPressedThisFrame();
    }
    #endregion
    #region Coroutines
    #endregion
}
