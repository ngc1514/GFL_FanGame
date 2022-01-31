using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Empty weapon - 0 ammo. Made it IReloadable so its easier to debug for now. 
 */

public class NullWeapon : Weapon //, IReloadable
{
    public override string Name { get; set; }
    public override bool IsShootable { get; } = false;
    public override float Rpm { get; set; } = 0f;

    public override int CurrentAmmo { get; set; } = 0;
    public override int TotalAmmoRemain { get; set; } = 0;
    public override  int MagSize { get; set; } = 0;

    public override void Attack()
    {
        Debug.Log("Useless attack");
    }

    public override void Reload()
    {
        Debug.Log("Null reload");
    }
}
