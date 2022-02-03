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
        Debug.Log("WeaponManager Initialized");
        UIController.Instance.UpdateDebug("WeaponManager Initialized");
    }

    // FIXME: convert rpm int (789) to float (0.076f)
    public void CreateWeapon(Type weaponType, GameObject playerObject, List<Weapon> weaponSlot, string name, float rpm, int currentAmmo, int mag, int total)
    {
        Weapon thisWeapon = (Weapon)playerObject.AddComponent(weaponType);
        string weaponTypeLower = weaponType.Name.ToLower();

        //Debug.LogWarning($"Creating thisWeapon {name}. Type: {weaponType}");
        //Debug.LogWarning($"This weapon: {thisWeapon}");
        //Debug.LogWarning($"audio_{name}");

        thisWeapon.weaponPrefab = ResourceManager.Instance.GetWeaponAndProjectile(weaponType, ($"{weaponTypeLower}_{name}")).WeaponName;
        thisWeapon.projectilePrefab = ResourceManager.Instance.GetWeaponAndProjectile(weaponType, ($"{weaponTypeLower}_{name}")).ProjectileName;
        thisWeapon.weaponAudio = ResourceManager.Instance.GetWeaponAudio($"audio_{name}");

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

        weaponSlot.Add(thisWeapon);

        //return thisWeapon;
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