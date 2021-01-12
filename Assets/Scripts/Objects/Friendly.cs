using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************************************

Friendly object for each friendly doll
   
- name
    - name for the prefab file

- EchelonData echelonBelongs
    - (echIdx, posIdx)
        - ech_idx: echelon index, which echelon member belongs to 
                    0 for unassigned.
        - pos_idx: member index in its echelon
                    0 for unassigned.

************************************************************************************/

public class Friendly : MonoBehaviour
{
    // FIXME: do i need singleton for friendly?
    public static Friendly currentFriendly;


    public float unitID = 0;
    private int _echIdx { get; set; }
    private int _posIdx { get; set; }
    public int multc = 1; // x1 ~ x5
    public static GameObject friendlyObj;
    public Rigidbody rb;

    // TODO: test


    private void Awake()
    {
        currentFriendly = this;

        //friendlyObj = Resources.Load("Prefabs/ump9") as GameObject;
        //rb = GameObject.Find("Ump9_capsule_collider").GetComponent<Rigidbody>();

        var rand = new System.Random();
        unitID = rand.Next(101);
    }


    //public static Friendly NewFriendly()
    //{
    //    //GameObject go = new GameObject("friendly");
    //    Friendly friendly = friendlyObj.AddComponent<Friendly>();
    //    //var rand = new System.Random();
    //    //unitID = rand.Next(101);
    //    return friendly;
    //}

    
    // FIXME: do i need this
    public bool HasCoordAssigned { get; set; } = false;

    public void SetEchIdx(int echIndexToSet)
    {
        if(echIndexToSet < PlayerPrefs.GetInt("MaxEchelonSlot"))
        {
            _echIdx = echIndexToSet;
        }
        else
        {
            Debug.LogError("Member's echelon index exceeded maximum echelon slot!");
        }
    }

 
    // FIXME: assume echelon is not full yet and only use this when adding NOT SWAPPING
    public void SetPosIdxForAddedMember(int posIdxToSet)
    {
        if (posIdxToSet > 0 && posIdxToSet < 5)
        {
            _posIdx = posIdxToSet;
        }
    }


    // TODO: swap unit

}
