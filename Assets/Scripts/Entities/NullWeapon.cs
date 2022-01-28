using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Empty weapon. 
 */

public class NullWeapon : Weapon
{
    public NullWeapon() { }

    public static NullWeapon CreateWeapon(GameObject playerObject)
    {
        NullWeapon thisWeapon = playerObject.AddComponent<NullWeapon>();
        thisWeapon.Name = "nullweapon";
        thisWeapon.MagSize = 0;
        thisWeapon.CurrentAmmo = 0;
        thisWeapon.TotalAmmoRemain = 0;
        return thisWeapon;
    }

}
