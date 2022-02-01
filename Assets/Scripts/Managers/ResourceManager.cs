using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-2)]
public class GameObjectManager : MonoBehaviour
{
    #region Singleton
    private static GameObjectManager _instance;
    public static GameObjectManager Instance { get { return _instance; } }
    #endregion

    #region weapon path
    // weapon path
    //private readonly string weaponResourcePath = "Model/Weapon/";
    //private readonly string gunResourcePath = "Model/Weapon/Gun/";
    //private readonly string meleeResourcePath = "Model/Weapon/Melee/";

    private readonly string rifleResourcePath = "Model/Weapon/Gun/Rifle/";

    // projectile path
    private readonly string projectileResourcePath = "Model/Weapons/Projectile/";
    #endregion


    #region character path
    private readonly string charResourcePath = "Models/Weapons/";
    #endregion

    // audio path see AudioManager

    #region sprites path
    private readonly string spritesPath = "Sprites/";
    #endregion

    // TODO: automatically load all resources under Resources/ and auto generate Properties name code
    #region objects dictionary
    public Dictionary<string, GameObject> WeaponDict { get; set; } = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> ProjectileDict { get; set; } = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> CharDict { get; set; } = new Dictionary<string, GameObject>();
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


    // Start is called before the first frame update
    void Start()
    {
        //pooledObjects = new List<GameObject>();
        //GameObject temp;
        //for(int i=0; i<amountToPool; i++)
        //{
        //    temp = Instantiate(objectToPool);
        //    temp.SetActive(false);
        //    pooledObjects.Add(temp);
        //}

    }


    // TODO: init basic resouces needed for like menu and stuffs
    //void InitBasicResources()
    //{
        
    //}


    // TODO: init battle required resource (seperate those Init methods to save RAM i guess
    // TODO: check for null?
    void LoadBattleResource()
    {
        Debug.Log("Loading basic resources");

        #region weapons models
        // rifle
        GameObject rifle_m4a1 = Resources.Load<GameObject>(rifleResourcePath + "m4a1");
        WeaponDict.Add(rifle_m4a1.name, rifle_m4a1);
        #endregion

        #region projectile models
        GameObject projectile_rifle = Resources.Load<GameObject>(projectileResourcePath + "projectile_rifle");
        ProjectileDict.Add(projectile_rifle.name, projectile_rifle);
        #endregion

        #region characters models
        GameObject char_ump9 = Resources.Load<GameObject>(charResourcePath + "ump9");
        CharDict.Add(char_ump9.name, char_ump9);
        #endregion


        #region sprites needed
        Sprite sprite_rifle_bullet_hole = Resources.Load<Sprite>(spritesPath + "rifle_bullet_hole");
        SpriteDict.Add(sprite_rifle_bullet_hole.name, sprite_rifle_bullet_hole);
        #endregion
    }


    // TODO: return resource gameobject from the dictionary
    //public GameObject GetResource<T>(Type objType, string objName)
    //{
    //    if(objType == typeof(Weapon))
    //    {
    //        return WeaponDict[objName];
    //    }
    //    else if(objType == typeof())
    //    else
    //    {

    //    }


    //    //switch (objType)
    //    //{
    //    //    case Type Weapon:
    //    //        return WeaponDict[objName];
    //    //    case Type GameObject:
    //    //        break;
    //    //    default:
    //    //        break;
    //    //}
    //}

    



    // Update is called once per frame
    //void Update()
    //{
        
    //}
}