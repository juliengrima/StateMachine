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
    bool _isJumpIsPressed;
    //Setter - Getter
    public Rigidbody Rb { get => _rb; }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _jumpHeight = 200f;
        _rb = transform.parent.GetComponentInChildren<Rigidbody>();
    }

    #endregion
    #region Methods
    private void FixedUpdate()
    {
        
    }
    public void Jump(InputActionReference _jump)
    {
        _isJumpIsPressed = _jump.action.WasPerformedThisFrame();
        //Debug.Log("Add Force to Rb Player");
        Rb.AddForce(Vector2.up * _jumpHeight);
    }
    #endregion
    #region Coroutines
    #endregion
}
