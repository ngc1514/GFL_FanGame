using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Knife : Weapon //, ISwingable
{
    public override string Name { get; set; }
    public override bool IsShootable { get; } = false;
    public override float Rpm { get; set; }
    public override int CurrentAmmo { get; set; }
    public override int TotalAmmoRemain { get; set; }
    public override int MagSize { get; set; }

    public override void Attack()
    {
        Debug.Log("Swing");
    }

    public override void Reload()
    {
        Debug.Log("Knife can't be reloaded");
    }
}
