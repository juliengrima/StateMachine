using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    #region Champs
    [Header("Components")]
    [SerializeField] Inventory _inventory;
    [Header("Events")]
    [SerializeField] UnityEvent _onUsed;
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _inventory = transform.parent.GetComponentInChildren<Inventory>();
    }
    
    #endregion
    #region Methods
    private void OnTriggerEnter(Collider other)
    {
        var item = gameObject;
        _inventory.AddItem(item);
        _onUsed.Invoke();
    }
    #endregion
    #region Coroutines
    #endregion
}
