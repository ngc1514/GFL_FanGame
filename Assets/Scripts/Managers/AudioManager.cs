using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }
    #endregion

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
    }

    // TODO: implement sound later. Need to get the sound source from corresponding gun.
    public void PlayGunSound(Weapon gun)
    {
        if(gun.audioSource == null)
        {
            Debug.LogError("No gun audio source found!");
        }
        gun.audioSource.Play();
    }
}
