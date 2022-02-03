using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Use this manager to spawn/kill player (or npc?)
 */

[DefaultExecutionOrder(-1)]
public class PlayerManager : MonoBehaviour
{
    #region Singleton
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
    #endregion

    // TODO: spawn entire Play object
    [SerializeField] private Player player;

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

        if (player == null)
        {
            player = Object.FindObjectOfType<Player>();
            if (player == null)
            {
                Debug.LogError("CurrentPlayer still null!");
            }
            else
            {
                Debug.Log($"Acquired player: {player.gameObject.name}");
                UIController.Instance.UpdateDebug($"Acquired player: {player.gameObject.name}");
            }
        }
        Debug.Log("PlayerManager Initialized");
        UIController.Instance.UpdateDebug("PlayerManager Initialized");
    }


    public Player GetCurrentPlayer()
    {
        return player;
    }

}