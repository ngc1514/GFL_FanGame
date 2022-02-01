using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script holds individual player information (maybe improvement in future)
 * Each individual player will have a player script attached
 */

public class Player : MonoBehaviour
{
    public int Health { get; private set; } = 1000;
    public int CurrentWeaponIndex { get; private set; } // 0-main, 1-side, 2-melee?
    protected Dictionary<string, Weapon> weaponSlotDict = new Dictionary<string, Weapon>();

    // Used to limit fire rate
    public bool CanShootNext { get; set; } = true;
    public bool IsHoldingFire { get; set; } = false;


    // TODO: Instantiate weapon object to player's parent and assign model ETC in the future
    private void Awake()
    { 
        // Test to spawn weapon at beginning
        Weapon main = WeaponManager.Instance.CreateWeapon(typeof(Rifle), this.gameObject, "m4a1", 0.076f, 30, 30, 90);  //Weapon.CreateWeapon(this.gameObject, "m4a1", 0.076f, 30, 30, 90);
        Weapon side = WeaponManager.Instance.CreateWeapon(typeof(Knife), this.gameObject, "naifu", 1, 30, 30, 90); //NullWeapon.CreateWeapon(this.gameObject);
        NullWeapon melee = WeaponManager.Instance.CreateNullWeapon(this.gameObject); // NullWeapon.CreateWeapon(this.gameObject);

        weaponSlotDict.Add("main", main);
        weaponSlotDict.Add("side", side);
        weaponSlotDict.Add("melee", melee);

        //foreach (KeyValuePair<string, Weapon> kvp in weaponSlotDict)
        //{
        //    Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value.Name) + "\n");
        //}
    }

    public Dictionary<string, Weapon> GetWeaponSlotDict()
    {
        return weaponSlotDict;
    }

    public Weapon GetCurrentWeapon()
    {
        //Debug.Log($"CurrentWeapon index: {CurrentWeaponIndex}, name: {weaponSlotDict["main"].Name}");

        switch (CurrentWeaponIndex)
        {
            case 0: return weaponSlotDict["main"];
            case 1: return weaponSlotDict["side"];
            case 2: return weaponSlotDict["melee"];
            default:
                Debug.LogError("You are trying to access a weapon slot index that doesn't exist!");
                return new NullWeapon();
        }
    }


    // TODO: scroll up to index-1, scroll down to index+1, range [0-2]
    // TODO: add touch button to switch weapon
    public void SwitchWeapon()
    {
        CurrentWeaponIndex = Mathf.Abs((CurrentWeaponIndex - 1) % 3);

        // TODO: call UIManager to change weapon UI, ammo, etc
        // TODO: then call PlayerController to swtich current weapon? - animation? but i thought it is done here? 
    }





    // TODO: pick up weapon? throw weapon? 
    void ThrowWeapon()
    {

    }

    void PickUpWeapon()
    {

    }


}
