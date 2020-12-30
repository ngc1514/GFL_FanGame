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

public class Friendly
{
    public int EchIdx { get; set; }
    public int PosIdx { get; set; }
    public string Name { get; set; }
    public int multc = 1; // x1 ~ x5
    public GameObject prefabObj;




    // FIXME: do i need this
    public bool HasCoordAssigned { get; set; } = false;



}
