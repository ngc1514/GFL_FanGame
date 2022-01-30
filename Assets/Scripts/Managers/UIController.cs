using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Singleton
    private static UIController _instance;
    public static UIController Instance { get { return _instance; } }
    #endregion

    [SerializeField] private Text ammoCountText;
    [SerializeField] private PlayerManager playerManager;

    private Player currentPlayer;

    [SerializeField] public Text debugText;


    private void Awake()
    {
        #region Singleton Awake
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion

        if (playerManager == null)
        {
            Debug.LogError("playerManager is null. Finding... ");
            playerManager = Object.FindObjectOfType<PlayerManager>();
            if(playerManager == null)
            {
                Debug.LogError("UIController: CANNOT FIND PLAYERMANAGER");
            }
        }
        if (ammoCountText == null)
        {
            Debug.LogError("Ammo Text is null");
        }
        currentPlayer = playerManager.GetCurrentPlayer();
    }

    private void Start()
    {
        ammoCountText.text = string.Format("Ammo: {0}/{1}", currentPlayer.GetCurrentWeapon().CurrentAmmo, currentPlayer.GetCurrentWeapon().TotalAmmoRemain);
        //Debug.Log($"Ammo: {currentPlayer.GetCurrentWeapon().CurrentAmmo}, {currentPlayer.GetCurrentWeapon().TotalAmmoRemain}");
    }

    public void UpdateAmmoText() //InputAction.CallbackContext context)
    {
        //Debug.Log($"Action: {context.action}");
        //Debug.Log($"Ammo: {currentPlayer.GetCurrentWeapon().CurrentAmmo}, {currentPlayer.GetCurrentWeapon().TotalAmmoRemain}");
        ammoCountText.text = string.Format("Ammo: {0}/{1}", currentPlayer.GetCurrentWeapon().CurrentAmmo, currentPlayer.GetCurrentWeapon().TotalAmmoRemain);
    }




    public void UpdateDebug(string txt)
    {
        debugText.text += (txt + "\n");
    }

    public void ClearDebug()
    {
        debugText.text = "";
    }


}
    