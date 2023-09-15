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
        //Vector3 rayStart = transform.position + Vector3.forward * 0.7f;

        if (Physics.Raycast(rayStart, _player.forward, out RaycastHit hit, _rayDistance))
        {
            // Si le rayon touche un objet avec le tag "Ground" (ou un autre tag que vous utilisez pour représenter le sol),
            // définissez _isGrounded sur true.
            if (hit.collider.CompareTag("Enemy"))
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
