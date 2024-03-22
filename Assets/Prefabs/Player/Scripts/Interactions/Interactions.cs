using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Interactions : MonoBehaviour
{
    #region Champs
    [Header("Components")]
    [SerializeField] InputActionReference _action;
    [SerializeField] List<string> _tags;
    [Header("Fields")]
    [SerializeField] float _rayDistance;

    Transform _player;
    bool _use;
    public bool Use { get => _use; }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _rayDistance = 1.2f;
        _use = false;
    }
    private void Start()
    {
        _player = gameObject.transform;
    }
    // Update is called once per frame
    public void Interations()
    {
        Vector3 rayStart = transform.position;

        if (Physics.Raycast(rayStart, _player.forward, out RaycastHit hit, _rayDistance))
        {
            //Loop to list more tags
            foreach (string tags in _tags)
            {
                //test if collider tag = tags list
                if (hit.collider.tag == tags)
                {
                    Debug.Log($"Touché {hit.collider.name}");
                    var hited = hit.collider.name;
                    // Affichage du nom de l'item par Canvas
                    if (_action.action.WasPerformedThisFrame())
                    {
                        bool action = _action.action.WasPerformedThisFrame();
                        if (hit.collider.TryGetComponent(out IInteractable usable))
                        {
                            usable.Use(action);
                        }
                    }
                    _use = true;
                    Debug.DrawRay(rayStart, _player.forward * _rayDistance, Color.HSVToRGB(108, 52, 131));
                }
                else
                {
                    _use = false;
                    Debug.DrawRay(rayStart, _player.forward * _rayDistance, Color.red);
                }
            } 
        }
        else
        {
            // Si le rayon ne touche rien, définissez _isGrounded sur false.
            _use = false;
            Debug.DrawRay(rayStart, _player.forward * _rayDistance, Color.red);
        } 
    }
    #endregion
    #region Methods
    #endregion
    #region Coroutines
    #endregion
}
