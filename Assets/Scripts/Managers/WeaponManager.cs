using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Singleton
    private static WeaponManager _instance;
    public static WeaponManager Instance { get { return _instance; } }
    #endregion

    //protected Dictionary<string, GameObject> gunPrefabDict = new Dictionary<string, GameObject>();
    //public GameObject m4A1Prefab;
    //public GameObject rifleBulletPrefab;


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

    // FIXME: convert rpm int (789) to float (0.076f)
    public Weapon CreateWeapon(Type weaponType, GameObject playerObject, string name, float rpm, int currentAmmo, int mag, int total)
    {
        Weapon thisWeapon = (Weapon)playerObject.AddComponent(weaponType);
        //thisWeapon.bulletPrefab = bulletPrefab;
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
