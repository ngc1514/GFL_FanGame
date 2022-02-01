using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }
    #endregion


    #region audio path
    private readonly string weaponAudioPath = "Audio/Weapons/";
    #endregion

    public Dictionary<string, AudioSource> AudioDict { get; set; } = new Dictionary<string, AudioSource>();


    #region audio object
    // TODO: make different audio resource for different guns
    private AudioSource audio_rifle;
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
        Debug.Log("Initializing AudioManager");


        #region audio resources
        audio_rifle = Resources.Load<AudioSource>(weaponAudioPath + "ar_1p_01");
        AudioDict.Add("audio_rifle", audio_rifle);
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
