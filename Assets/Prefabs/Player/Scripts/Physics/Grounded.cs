using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Grounded : MonoBehaviour
{
    #region Champs
    [SerializeField] Rigidbody _player;
    [SerializeField] bool _isGrounded;
    [SerializeField] float _rayDistance;
    [SerializeField] List<string> _colliderList;

    public bool IsGrounded { get => _isGrounded; }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _isGrounded = false;
        _rayDistance = 0.3f;
        _player = transform.parent.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame

    #endregion
    #region Methods
    private void Update()
    {
        // Assurez-vous que la valeur Y est légèrement au-dessus du sol
        //Make sure Y value is is slightly above the ground
        Vector3 rayStart = transform.position;
        // Lance un rayon vers le bas
        //Make Ray DownWard
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, _rayDistance))
        {
            // Si le rayon touche un objet avec le tag "Ground" (ou un autre tag que vous utilisez pour représenter le sol),
            // définissez _isGrounded sur true.
            //if (hit.collider.CompareTag("Ground"))
            foreach (var _collider in _colliderList)
            {
                if (hit.collider.tag == _collider)
                {
                    _isGrounded = true;
                    Debug.DrawRay(rayStart, Vector3.down * _rayDistance, Color.green);
                }
                else
                {
                    _isGrounded = false;
                    Debug.DrawRay(rayStart, Vector3.down * _rayDistance, Color.red);
                }
            }
        }
        else
        {
            // Si le rayon ne touche rien, définissez _isGrounded sur false.
            _isGrounded = false;
            Debug.DrawRay(rayStart, Vector3.down * _rayDistance, Color.red);
        }
    }
    #endregion
    #region Coroutines
    #endregion
}

// Utilisation du Physics Overlap SPHERE
public class ExampleClass : MonoBehaviour
{
    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("AddDamage");
        }
    }
}
