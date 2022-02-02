using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-3)]
public class ResourceManager : MonoBehaviour
{
    #region Singleton
    private static ResourceManager _instance;
    public static ResourceManager Instance { get { return _instance; } }
    #endregion

    #region weapon path
    private readonly string rifleResourcePath = "Models/Weapons/Guns/Rifles/";
    private readonly string pistolResourcePath = "Models/Weapons/Guns/Pistols/";

    private readonly string projectileResourcePath = "Models/Weapons/Projectiles/";
    #endregion

    #region character path
    private readonly string charResourcePath = "Models/Characters/";
    #endregion

    #region audio path
    private readonly string audioPath = "Audio/Weapon/";
    #endregion

    #region sprites path
    private readonly string spritesPath = "Sprites/";
    #endregion

    // TODO: automatically load all resources under Resources/ and auto generate Properties name code
    #region objects dictionary
    public Dictionary<string, (GameObject WeaponName, GameObject ProjectileName)> WeaponAndProjectileDict { get; set; } = new Dictionary<string, (GameObject WeaponName, GameObject ProjectileName)>();
    public Dictionary<string, GameObject> CharDict { get; set; } = new Dictionary<string, GameObject>();
    public Dictionary<string, AudioClip> AudioDict { get; set; } = new Dictionary<string, AudioClip>();
    public Dictionary<string, Sprite> SpriteDict { get; set; } = new Dictionary<string, Sprite>();
    #endregion


    // TODO: object pools, read docs: https://learn.unity.com/tutorial/introduction-to-object-pooling#5ff8d015edbc2a002063971d
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    

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



        //InitBattleResource();
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
        Debug.Log("Loading basic resources");
        UIController.Instance.UpdateDebug("Loading basic resources");

        #region weapons models
        // rifle
        GameObject rifle_m4a1 = Resources.Load<GameObject>(rifleResourcePath + "rifle_m4a1");
        GameObject projectile_m4a1 = Resources.Load<GameObject>(projectileResourcePath + "projectile_m4a1");
        WeaponAndProjectileDict.Add(rifle_m4a1.name, (rifle_m4a1, projectile_m4a1));


        // pistol
        GameObject pistol_m1911 = Resources.Load<GameObject>(pistolResourcePath + "pistol_m1911");
        GameObject projectile_m1911 = Resources.Load<GameObject>(projectileResourcePath + "projectile_m1911");
        WeaponAndProjectileDict.Add(pistol_m1911.name, (pistol_m1911, projectile_m1911));
        #endregion


        #region characters models
        GameObject ump9 = Resources.Load<GameObject>(charResourcePath + "ump9");
        CharDict.Add(ump9.name, ump9);
        #endregion


        #region sprites needed
        Sprite sprite_rifle_bullet_hole = Resources.Load<Sprite>(spritesPath + "rifle_bullet_hole");
        SpriteDict.Add(sprite_rifle_bullet_hole.name, sprite_rifle_bullet_hole);
        #endregion


        #region audio object
        AudioClip audio_rifle = Resources.Load<AudioClip>(audioPath + "ar_1p_01");
        AudioDict.Add("audio_rifle", audio_rifle);
        #endregion
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
        return AudioDict[gunAudioName];
    }

}