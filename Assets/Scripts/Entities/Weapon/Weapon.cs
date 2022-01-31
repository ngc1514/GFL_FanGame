using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public abstract string Name { get; set; }
    public abstract bool IsShootable { get; }
    public abstract float Rpm { get; set;  }
    public abstract int CurrentAmmo { get; set; }
    public abstract int TotalAmmoRemain { get; set; }
    public abstract int MagSize { get; set; }

    public AudioSource audioSource;
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;

    public abstract void Attack();
    public abstract void Reload();
}
