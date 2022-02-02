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
    private readonly string weaponAudioPath = "Audio/Weapon/";
    #endregion

    public Dictionary<string, AudioClip> AudioDict { get; set; } = new Dictionary<string, AudioClip>();

    #region audio object
    // TODO: make different audio resource for different guns
    private AudioClip audio_rifle;
    #endregion

    [SerializeField] private AudioSource testSource;


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
        audio_rifle = Resources.Load<AudioClip>(weaponAudioPath + "ar_1p_01");
        AudioDict.Add("audio_rifle", audio_rifle);
        #endregion
    }

    // TODO: implement sound later. Need to get the sound source from corresponding gun.
    public void PlayGunSound() //string gunName)
    {
        //if(gun.audioSource == null)
        //{
        //    Debug.LogError("No gun audio source found!");
        //}
        //gun.audioSource.Play();
        //testSource.clip

        testSource.PlayOneShot(audio_rifle, 0.1f);
    }
}
