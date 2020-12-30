using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    #region Singleton
    private static PopUpManager _instance;
    public static PopUpManager Instance
    {
        get
        {
            if (!_instance)
            {
                Debug.LogWarning("An instance of PopUpManager is needed. Creating one now...");
                _instance = new GameObject("ControlManager", typeof(PopUpManager)).GetComponent<PopUpManager>();
            }

            return _instance;
        }
    }
    #endregion



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
