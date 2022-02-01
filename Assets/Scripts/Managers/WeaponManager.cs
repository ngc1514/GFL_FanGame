using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-1)]
public class WeaponManager : MonoBehaviour
{
    #region Singleton
    private static WeaponManager _instance;
    public static WeaponManager Instance { get { return _instance; } }
    #endregion

    //protected Dictionary<string, GameObject> gunPrefabDict = new Dictionary<string, GameObject>();
    //public GameObject m4A1Prefab;
    //public GameObject rifleBulletPrefab;


    // FIXME: FOR TESTING ONLY for now. get weapon and projectile prefab from ResourceManager
    [SerializeField] public GameObject weaponPrefab;
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public Transform barrelTransform;
    [SerializeField] public Transform projectileParent;

    [SerializeField] public AudioSource RifleAudioSource;


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
        Debug.Log("Initializing WeaponManager");
    }

    // FIXME: convert rpm int (789) to float (0.076f)
    public Weapon CreateWeapon(Type weaponType, GameObject playerObject, string name, float rpm, int currentAmmo, int mag, int total)
    {
        string baseTypeLower = Util.LowerFirstLetter(weaponType.Name);
        Debug.Log($"weaponType Name: [{weaponType.Name}], baseType Lower is: {baseTypeLower}");

        Weapon thisWeapon = (Weapon)playerObject.AddComponent(weaponType);
        //object thisWeapon = Activator.CreateInstance(weaponType);

        //string wP = weaponType.Name + "_" + name;
        //string pP = "Projectile_" + baseTypeLower;
        //string aS = "Audio_" + baseTypeLower;
        //Debug.Log($"{wP}, {pP}, {aS}");

        // FIXME: get prefab from ResourceManager 
        thisWeapon.weaponPrefab = weaponPrefab; //(GameObject)PrefabManager.Instance.GetType().GetProperty(weaponType.Name + "_" + name).GetValue(PrefabManager.Instance.GetType(), null);
        thisWeapon.projectilePrefab = projectilePrefab; //(GameObject)PrefabManager.Instance.GetType().GetProperty("Projectile_" + baseTypeLower).GetValue(PrefabManager.Instance.GetType(), null);
        thisWeapon.audioSource = RifleAudioSource; //(AudioSource)PrefabManager.Instance.GetType().GetProperty("Audio_" + baseTypeLower).GetValue(PrefabManager.Instance.GetType(), null);

        thisWeapon.Name = name;
        thisWeapon.MagSize = mag;
        thisWeapon.Rpm = rpm;

        if (currentAmmo > mag)
        {
            Debug.LogError("Current ammo is more than Mag size!! Setting to mag size.");
            thisWeapon.CurrentAmmo = mag;
        }
        else
        {
            thisWeapon.CurrentAmmo = currentAmmo;
        }
        thisWeapon.TotalAmmoRemain = total;
        return thisWeapon;
    }


    public NullWeapon CreateNullWeapon(GameObject playerObject)
    {
        NullWeapon thisWeapon = playerObject.AddComponent<NullWeapon>();
        thisWeapon.Name = "nullweapon";
        thisWeapon.MagSize = 0;
        thisWeapon.CurrentAmmo = 0;
        thisWeapon.TotalAmmoRemain = 0;
        return thisWeapon;
    }
}
