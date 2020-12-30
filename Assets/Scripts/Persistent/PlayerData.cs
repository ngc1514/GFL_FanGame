using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    //TODO: in the future save all data in a file and parse saved data before starting game 


    #region Singleton
    private static ControlManager _instance;
    public static ControlManager Instance
    {
        get
        {
            if (!_instance)
            {
                Debug.LogWarning("An instance of ControlManager is needed. Creating one now...");
                _instance = new GameObject("ControlManager", typeof(ControlManager)).GetComponent<ControlManager>();
            }

            return _instance;
        }
    }
    #endregion



    public static int MaxEchelonSlot { get; private set; } = 1;
    public static List<Echelon> echelonList = new List<Echelon>(); 
}
