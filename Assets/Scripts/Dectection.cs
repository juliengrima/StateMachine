using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    #region Champs
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Chargez la scène actuelle à nouveau
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // Affichez "GAGNE !" (vous pouvez le faire d'une manière appropriée)
            Debug.Log("PERDU !");
        }
    }
    #endregion
    #region Methods

    #endregion
    #region Coroutines
    #endregion
}
