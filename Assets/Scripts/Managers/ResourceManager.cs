using System;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-3)]
public class ResourceManager : MonoBehaviour
{
    #region Singleton
    private static ResourceManager _instance;
    public static ResourceManager Instance { get { return _instance; } }
    #endregion

    [SerializeField] private List<GameObject> pooledObjects;
    [SerializeField] private GameObject thePool;


    #region paths
    // weapon 
    private readonly string rifleResourcePath = "Models/Weapons/Guns/Rifles/";
    private readonly string pistolResourcePath = "Models/Weapons/Guns/Pistols/";

    // projectile 
    private readonly string projectileResourcePath = "Models/Weapons/Projectiles/";

    // character 
    private readonly string charResourcePath = "Models/Characters/";

    // audio 
    private readonly string audioPath = "Audios/Weapons/";

    //sprites 
    private readonly string spritesPath = "Sprites/";
    #endregion


    // TODO: automatically load all resources under Resources/ and auto generate Properties name code
    #region objects dictionary
    public Dictionary<string, (GameObject WeaponName, GameObject ProjectileName)> WeaponAndProjectileDict { get; set; } = new Dictionary<string, (GameObject WeaponName, GameObject ProjectileName)>();
    public Dictionary<string, GameObject> CharDict { get; set; } = new Dictionary<string, GameObject>();
    public Dictionary<string, AudioClip> AudioDict { get; set; } = new Dictionary<string, AudioClip>();
    public Dictionary<string, Sprite> SpriteDict { get; set; } = new Dictionary<string, Sprite>();
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

        FillObjectPool();

        LoadBattleResource();
    }


    // TODO: init basic resouces needed for things like menu and stuffs
    //void InitBasicResources()
    //{
        
    //}


    // TODO: init battle required resource (seperate those Init methods to save RAM i guess
    // TODO: check for null?
    void LoadBattleResource()
    {
        Debug.Log("Loading battle resources");
        UIController.Instance.UpdateDebug("Loading basic resources");

        #region weapons models
        // rifle
        GameObject rifle_m4a1 = Resources.Load<GameObject>(rifleResourcePath + "rifle_m4a1");
        GameObject projectile_m4a1 = Resources.Load<GameObject>(projectileResourcePath + "projectile_m4a1");
        WeaponAndProjectileDict.Add(nameof(rifle_m4a1), (rifle_m4a1, projectile_m4a1));

        // pistol
        GameObject pistol_m1911 = Resources.Load<GameObject>(pistolResourcePath + "pistol_m1911");
        GameObject projectile_m1911 = Resources.Load<GameObject>(projectileResourcePath + "projectile_m1911");
        WeaponAndProjectileDict.Add(nameof(pistol_m1911), (pistol_m1911, projectile_m1911));
        #endregion


        #region characters models
        GameObject ump9 = Resources.Load<GameObject>(charResourcePath + "ump9");
        CharDict.Add(nameof(ump9), ump9);
        #endregion


        #region sprites needed
        Sprite sprite_rifle_bullet_hole = Resources.Load<Sprite>(spritesPath + "rifle_bullet_hole");
        SpriteDict.Add(nameof(sprite_rifle_bullet_hole), sprite_rifle_bullet_hole);
        #endregion


        #region audio object
        AudioClip audio_m4a1 = Resources.Load<AudioClip>(audioPath + "m4a1");
        AudioDict.Add(nameof(audio_m4a1), audio_m4a1);

        AudioClip audio_m1911 = Resources.Load<AudioClip>(audioPath + "m1911");
        AudioDict.Add(nameof(audio_m1911), audio_m1911);
        #endregion
    }



    public void FillObjectPool()
    {
        UnityEngine.Object[] allModels;

        allModels = Resources.LoadAll("Models", typeof(GameObject));

        GameObject tmp;
        foreach(var mod in allModels)
        {
            tmp = Instantiate((GameObject)mod);
            tmp.transform.parent = thePool.transform;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }




    // TODO: return resource gameobject from the dictionary 
    // FIXME: validation? check "rifle_" or "pistol_"?
    public (GameObject WeaponName, GameObject ProjectileName) GetWeaponAndProjectile(Type objType, string objName)
    {
        if (objType.IsSubclassOf(typeof(Weapon)))
        {
            return WeaponAndProjectileDict[objName];
        }
        else
        {
            throw new Exception($"Input type {objType} is not a weapon!");
        }
    }


    public GameObject GetCharModel(string charName)
    {
        return CharDict[charName];
    }


    public AudioClip GetWeaponAudio(string gunAudioName)
    {
        //Debug.LogWarning($"Fetching audio: {gunAudioName}");
        return AudioDict[gunAudioName];
    }


}