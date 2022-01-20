using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerManager))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int health = 1000;
    [SerializeField] private Dictionary<string, string> weapons;
    [SerializeField] private Text ammoCountText;

    [SerializeField] private PlayerInputActions playerInput;

    private int magSize = 30;
    private int currentAmmo = 30;
    private int totalAmmoRemain = 300;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Start()
    {
        ammoCountText.text = string.Format("Ammo: {0}/{1}", currentAmmo, totalAmmoRemain);
    }

    void Update()
    {
        
    }

    // FIXME: Ammo count is stupid for now. Brain dont work. Will make a smarter version.
    public void FireOrReload()
    {
        if (currentAmmo != 0)
        {
            currentAmmo -= 1;
        } 
        else
        {
            Reload();          
        }
        ammoCountText.text = string.Format("Ammo: {0}/{1}", currentAmmo, totalAmmoRemain);
    }

    public void Reload()
    {
        int need = magSize - currentAmmo;
        if (currentAmmo != magSize)
        {
            if (totalAmmoRemain != 0)
            {
                if (totalAmmoRemain > need)
                {
                    totalAmmoRemain -= need;
                    currentAmmo = magSize;
                }
                else
                {
                    currentAmmo = totalAmmoRemain;
                    totalAmmoRemain = 0;
                }
            }
            else
            {
                Debug.Log("No more ammo");
            }
            Debug.Log(string.Format("Reloading! {0}/{1}", currentAmmo, totalAmmoRemain));
            ammoCountText.text = string.Format("Ammo: {0}/{1}", currentAmmo, totalAmmoRemain);
        }
    }
}