using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandUIController : MonoBehaviour
{
    [SerializeField] GameObject dummyUI;
    public bool IsUIShown = false;

    private void Start()
    {
        dummyUI.SetActive(false);
        BattleEvent.current.OnFriendlyClicked += CommandUIShowOrHide;
    }



    // TODO: finish UI design and etc
    private void CommandUIShowOrHide(object sender, EventArgs e)
    {
        IsUIShown = !IsUIShown;
        Debug.LogWarning(IsUIShown ? "Showing": "Hiding");
        dummyUI.SetActive(IsUIShown);
    }

}
