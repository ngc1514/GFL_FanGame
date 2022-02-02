using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    #region wepon properties
    public abstract string Name { get; set; }
    public abstract bool IsShootable { get; }
    public abstract float Rpm { get; set; }
    public abstract int CurrentAmmo { get; set; }
    public abstract int TotalAmmoRemain { get; set; }
    public abstract int MagSize { get; set; }

    public AudioSource audioSource;
    public GameObject weaponPrefab;
    public GameObject projectilePrefab;
    #endregion


    #region IWeapon interface methods
    public abstract bool Attack();
    public abstract bool Reload();
    #endregion
}