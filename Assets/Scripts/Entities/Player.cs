using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script holds individual player information (maybe improvement in future)
 * Each individual player will have a player script attached
 */

public class Player : MonoBehaviour
{
    [SerializeField] public GameObject WeaponWrapper;
    [SerializeField] public GameObject ModelWrapper;

    public int Health { get; private set; } = 1000;
    public List<Weapon> weaponSlot = new List<Weapon>();
    public int CurrentWeaponIndex { get; private set; } // 0-main, 1-side, 2-melee?

    // Used to limit fire rate
    public bool CanShootNext { get; set; } = true;
    public bool IsHoldingFire { get; set; } = false;


    // TODO: Instantiate weapon object to player's parent and assign model ETC in the future
    private void Awake()
    {
        GameObject charModel = ResourceManager.Instance.GetCharModel("ump9");
        WeaponManager.Instance.CreateWeapon(typeof(Rifle), this.gameObject, weaponSlot, "m4a1", 0.076f, 30, 30, 90);  

        GameObject weaponModelSpawn = Instantiate(weaponSlot[0].weaponPrefab);
        weaponModelSpawn.transform.parent = WeaponWrapper.transform;
        weaponSlot[0].muzzleTransform = weaponModelSpawn.GetComponentInChildren<Transform>().Find("muzzle");

        Instantiate(charModel).transform.parent = ModelWrapper.transform;

        CurrentWeaponIndex = 0;
    }


    //
    public Weapon GetCurrentWeapon()
    {
        //Debug.Log($"CurrentWeapon index: {CurrentWeaponIndex}, name: {weaponSlotDict["main"].Name}");
        //switch (CurrentWeaponIndex)
        //{
        //    case 0: return weaponSlotDict["main"];
        //    case 1: return weaponSlotDict["side"];
        //    case 2: return weaponSlotDict["melee"];
        //    default:
        //        Debug.LogError("You are trying to access a weapon slot index that doesn't exist!");
        //        return new NullWeapon();
        //}

        // return weaponSlot[CurrentWeaponIndex];
        return weaponSlot[0];
    }
}
