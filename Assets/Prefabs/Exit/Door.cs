using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    #region Champs
    [Header("Components")]
    //[SerializeField] AudioMixer _mixer;
    //[SerializeField] AudioSource _source;
    //[SerializeField] AudioClip _clip;
    //[SerializeField] Animator _animator;
    [Header("Player_omponents")]
    [SerializeField] Inventory _inventory;
    [Header("Events")]
    [SerializeField] UnityEvent _onUsed;
    [SerializeField] UnityEvent _onOpen;

    [SerializeField] float _wait;

    int actionCount = 0;
    string _key;
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _wait = 1.5f;
        _inventory = transform.parent.GetComponentInChildren<Inventory>();
    }


    #endregion
    #region Methods
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody == null) return;

        if (collision.rigidbody.gameObject.CompareTag("Player"))
        {

            Debug.Log("Le player rentre en collision avec la porte");
            Debug.Log("Voyons si le joueur a la clé!");
            //Recup l'inventory et check GameObjects
            foreach (var key in _inventory.itemInventory)
            {
                _key = key.gameObject.tag;
                // If a GameObject Tag = Key 
                if (_key == "Key")
                {
                    _onUsed.Invoke();
                    StartCoroutine(NightClub());
                }
                else
                {
                    Debug.Log("Le joueur n'a pas la clé !");
                }
            }
        }  
    }
    #endregion
    #region Coroutines
    IEnumerator NightClub()
    {
        yield return new WaitForSeconds(_wait);
        //_mixer.SetFloat("CutOfLowpass", 5000f);
        //_mixer.SetFloat("CutLowMusic", 0f);
        _onOpen.Invoke();
    }
    IEnumerator NightClubClose()
    {
        yield return new WaitForSeconds(_wait);
        //_mixer.SetFloat("CutOfLowpass", 1000f);
        //_mixer.SetFloat("CutLowMusic", -35f);
    }
    #endregion
}
