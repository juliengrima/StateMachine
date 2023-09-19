using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dectection : MonoBehaviour
{
    #region Champs
    [SerializeField] EntityMove _entityMove;
    [SerializeField] WayPoint _waypoints;
    [SerializeField] float _wait;
    [SerializeField] float _checkSpeed;

    private Light spotlightLight; // Référence au composant Light du Spotlight
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _wait = 10f;
        _checkSpeed = 2f;
    }

    private void Start()
    {
        // Obtenez la référence au composant Light du Spotlight
        spotlightLight = GetComponent<Light>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.attachedRigidbody == null) return;
        if (collision.attachedRigidbody.gameObject.CompareTag("Player"))
        {
            if (_entityMove.IsCrouching == true)
            {
                // Changez la couleur du spotlight
                spotlightLight.color = Color.yellow; // Par exemple, changez la couleur en rouge

                StartCoroutine(checkPlayer());
                Debug.Log("Surveillance Accrue !"); 
            }
            else
            {
                // Chargez la scène actuelle à nouveau
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                // Affichez "GAGNE !" (vous pouvez le faire d'une manière appropriée)
                Debug.Log("PERDU !");
            }
        }
    }

    //private void OnTriggerExit(Collider collision)
    //{
    //    if (collision.attachedRigidbody == null) return;
    //    if (collision.attachedRigidbody.gameObject.CompareTag("Player"))
    //    {
    //        _waypoints.Speed *= _checkSpeed;
    //    }
    //}

    IEnumerator checkPlayer()
    {
        yield return new WaitForSeconds(_wait);
        // Changez la couleur du spotlight
        spotlightLight.color = Color.HSVToRGB(90, 255, 57); // Par exemple, changez la couleur en rouge

    }
    #endregion
    #region Methods

    #endregion
    #region Coroutines
    #endregion
}
