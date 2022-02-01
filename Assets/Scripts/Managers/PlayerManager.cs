using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Use this manager to spawn/kill player (or npc?)
 */

[DefaultExecutionOrder(-1)]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player player;
    //[SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
        //TODO: instantiate(spawn) a new player with new position, weapon prefab, and audio etc
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
            }
            //Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            //player = playerPrefab.GetComponent<Player>();
        }
        Debug.Log("Initializing PlayerManager");
    }

    //private void Start()
    //{
    //    Debug.Log("Activating!!!!");
    //    player.transform.parent.gameObject.SetActive(true);
    //}


    public Player GetCurrentPlayer()
    {
        return player;
    }

}