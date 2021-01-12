using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************************************

Holds echelon data

//TODO: in the future use player pref

************************************************************************************/

public class PlayerData: MonoBehaviour
{
    public static PlayerData current;
    private static int _maxEchelonSlot = 1;
    public static List<Echelon> echelonList = new List<Echelon>();


    // FIXME: this is for dev. Need to read data from player files in the future
    public static void InitPlayerData()
    {
        Debug.LogWarning("init player data");

        PlayerPrefs.SetInt("MaxEchelonSlot", _maxEchelonSlot);


        Echelon newTestEchelon = new Echelon();
        Friendly ump9 = new Friendly(); // FIXME: need to fix new
        //Friendly ump9 = Friendly.NewFriendly();
        newTestEchelon.AddMemeber(ump9);
    }



    public static int GetEchelonCount()
    {
        return echelonList.Count;
    }


    public void IncreaseMaxEchelonSlot()
    {
        _maxEchelonSlot += 1;
        PlayerPrefs.SetInt("MaxEchelonSlot", _maxEchelonSlot);
    }




}
