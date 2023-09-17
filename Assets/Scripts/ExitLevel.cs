using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    #region Champs
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {

        if (other.attachedRigidbody.gameObject.CompareTag("Player"))
        {
            // Chargez la scène actuelle à nouveau
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // Affichez "GAGNE !" (vous pouvez le faire d'une manière appropriée)
            Debug.Log("GAGNE !");
        }
    }
    #endregion
    #region Methods

    #endregion
    #region Coroutines
    #endregion
}
