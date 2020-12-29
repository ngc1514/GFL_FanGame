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
        - ech_idx: echelon index, which echelon the current member is in. 
                    0 for unassigned.
        - pos_idx: member index in its echelon. 
                    0 for unassigned.

************************************************************************************/

public class Friendly
{
    // holds echlon data of this friendly member 
    public class EchelonData
    {
        public int EchIdx { get; set; }
        public int PosIdx { get; set; }

        public EchelonData(int echIdx, int posIdx)
        {
            EchIdx = echIdx;
            PosIdx = posIdx;
        }
    }

    public string Name { get; set; }
    public EchelonData echelonBelongs;

    // FIXME: do i really need it here?
    public Coord assignedCoord; // assigned at the beginning of each stage



    //public GameObject friendlyPrefab;

    //public void InitPrefab()
    //{
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        GameObject spawnedEnemy = Instantiate(friendlyPrefab, enemySpawnPos, Quaternion.Euler(new Vector3(EneRotaX, EneRotaY + 180, EneRotaZ)));
    //    }
    //    else
    //    {
    //        Debug.LogError("Friendly: friendly name null or empty");
    //    }

    //}

}
