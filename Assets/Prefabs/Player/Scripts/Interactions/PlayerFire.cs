using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    #region Champs
    bool _isFireIsPressed;
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
    public void Fire(InputActionReference _fire)
    {
        _isFireIsPressed = _fire.action.WasPressedThisFrame();
    }
    #endregion
    #region Coroutines
    #endregion
}
