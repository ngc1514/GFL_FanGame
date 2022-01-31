using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    // TODO: Knife and melee should have ammo of inf, shoud i make a separate interface?
    int CurrentAmmo { get; }
    int TotalAmmoRemain { get; }
    int MagSize { get; }
    float Rpm { get; }

    void Attack();
    void Reload();
}
