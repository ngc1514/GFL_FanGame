using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rifle : Weapon, IShootable
{
    public override string Name { get; set; }
    public override bool IsShootable { get; } = true;
    public override float Rpm { get; set; }
    public override int CurrentAmmo { get; set; }
    public override int TotalAmmoRemain { get; set; }
    public override int MagSize { get; set; }

    // TODO: caliber future use
    //public int Caliber { get; set; } 

    //private void Start()
    //{
    //    if (this.projectilePrefab = null)
    //    {
    //        Debug.LogError("bulletPrefab is null");
    //    }
    //}

    public override void Attack()
    {
        if (CurrentAmmo > 0)
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


    public override void Reload()
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
