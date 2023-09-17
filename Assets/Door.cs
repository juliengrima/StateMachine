using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Door : MonoBehaviour
{
    #region Champs
    [Header("Components")]
    [SerializeField] AudioMixer _mixer;
    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip _clip;
    [SerializeField] Animator _animator;
    [SerializeField] Inventory _inventory;

    [SerializeField] float _wait;

    int actionCount = 0;
    string _key;
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _wait = 1.5f;
    }


    #endregion
    #region Methods
    private void OnCollisionEnter(Collision collision)
    {
        //Recup l'inventory et check GameObjects
        foreach (var key in _inventory.itemInventory)
        {
            _key = key.gameObject.tag;
            // If a GameObject Tag = Key 
            if (_key == "Key")
            {
                //if (action)
                //{
                    _animator.SetTrigger("Up"); //Play animation
                    //Test if player use more 2 time fight button
                    if (actionCount == 2) actionCount = 0;
                    actionCount++;
                    // Disable and enable Collider for takedamage
                    if (actionCount == 1)
                    {
                        StartCoroutine(NightClub());
                        _source.PlayOneShot(_clip);
                    }
                    else if (actionCount == 2)
                    {
                        StartCoroutine(NightClubClose());
                        _source.PlayOneShot(_clip);
                    }
                //}
            }
        }
    }
    #endregion
    #region Coroutines
    IEnumerator NightClub()
    {
        yield return new WaitForSeconds(_wait);
        _mixer.SetFloat("CutOfLowpass", 5000f);
        _mixer.SetFloat("CutLowMusic", 0f);
    }
    IEnumerator NightClubClose()
    {
        yield return new WaitForSeconds(_wait);
        _mixer.SetFloat("CutOfLowpass", 1000f);
        _mixer.SetFloat("CutLowMusic", -35f);
    }
    #endregion
}
