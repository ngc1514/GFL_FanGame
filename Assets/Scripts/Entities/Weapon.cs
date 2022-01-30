﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string Name { get; set; }
    public int CurrentAmmo { get; set; }
    public int TotalAmmoRemain { get; set; }
    public int MagSize { get; set; }
    public float Rpm { get; set; }
    public AudioSource audioSource;


    public static Weapon CreateWeapon(GameObject playerObject, string name, float rpm, int currentAmmo, int mag, int total)
    {
        Weapon thisWeapon = playerObject.AddComponent<Weapon>();
        thisWeapon.Name = name;
        thisWeapon.MagSize = mag;
        thisWeapon.Rpm = rpm;
        if(currentAmmo > mag)
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

    public void SetCurrentAmmo(int cur)
    {
        CurrentAmmo = cur;
    }

    public void FireOne()
    {
        if(CurrentAmmo > 0)
        {
            CurrentAmmo -= 1;
            Debug.Log(string.Format("Firing! {0}/{1}", CurrentAmmo, TotalAmmoRemain));
            UIController.Instance.UpdateDebug(string.Format("Firing! {0}/{1}", CurrentAmmo, TotalAmmoRemain));
        }
        else
        {
            Debug.Log("Mag Empty!");
        }
    }

    public void Reload()
    {
        int tempTotal = CurrentAmmo + TotalAmmoRemain;
        if (tempTotal >= 30)
        {
            TotalAmmoRemain -= (MagSize - CurrentAmmo);
            CurrentAmmo = 30;
        }
        else
        {
            CurrentAmmo += TotalAmmoRemain;
            TotalAmmoRemain = 0;
        }
        Debug.Log(string.Format("Reloading! {0}/{1}", CurrentAmmo, TotalAmmoRemain));
        UIController.Instance.UpdateDebug(string.Format("Reloading! {0}/{1}", CurrentAmmo, TotalAmmoRemain));
    }




}