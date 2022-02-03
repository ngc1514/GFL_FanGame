using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }
    #endregion

    [SerializeField] private AudioSource Source;

    private AudioClip cachedAudio;


    private void Awake()
    {
        #region Singleton Awake
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion
        Debug.Log("AudioManager Initialized");
    }

    // TODO: need more validation code
    public void PlayGunSound(Weapon curGun)
    {
        Source.PlayOneShot(curGun.weaponAudio, 0.1f);
    }
}
