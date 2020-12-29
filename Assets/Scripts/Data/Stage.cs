using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************************************

Hold data for a Stage object
- contains necessary data for map json file


- string stageName:
    - "1_1"
        - chapter 1 level 1

- string size:
    - "6_6"
        - a 6x6 map
            - scale: 0.6*0.6
            - x=3, y=0, z=3

- block
    - everything above 1/2 width are enemy blocks. below are friendly.
    - [(3,4)]
        - a block on enemy size located at (3,4)
    - [(1,1)]
        - a block on friendly size located at (1,1)

- enemyPos
    - determined by the map json file

- friendlyPos
    - assigned at the beginning of each level


************************************************************************************/

public class Stage
{
    public bool IsAssigned { get; private set; } = false; // by default is false
    private int _countAssigned = 0;

    public string Name { get; set; }
    public int Length { get; set; } //FIXME: need to re-adjust plane collider size
    public int Width { get; set; }

    public List<Enemy> enemyList;
    public List<Coord> blocks;
    public List<Coord> enemyPos;

    public Friendly[] friendlyList = new Friendly[5];

    // size is hardcoded to 5
    public Coord[] friendlyPos = new Coord[5]; // assigned at the beginning

    public bool IsEveryoneAssigned()
    {
        if (friendlyPos.Length == 5 && friendlyPos != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (friendlyPos[i] != null) // and need to validate pos
                {
                    IsAssigned = true;
                }
                else
                {
                    IsAssigned = false;
                    return IsAssigned;
                }
            }
        }
        else
        {
            IsAssigned = false;
            Debug.LogError("Something is wrong with friendlyPos array");
        }

        return IsAssigned;
    }



    public IEnumerator AssignEchePos()
    {
        if (_countAssigned < 0 || _countAssigned > 5)
        {
            Debug.LogError("Something wrong with countAssigned");
            IsAssigned = false;
            yield return null;
        }
        else
        {
            yield return WaitForAllToBeAssigned();
        }

    }

    private IEnumerator WaitForAllToBeAssigned()
    {
        bool done = false;
        while (!done)
        {
            if (Input.GetMouseButtonDown(0)) // && _countAssigned < 5)
            {
                //Debug.Log("Assigning: " + countAssigned);
                ControlManager ctrlManager = GameObject.FindGameObjectWithTag("ControlManager").GetComponent<ControlManager>();

                // TODO: record a coord 
                Coord clickedCoord = ctrlManager.GetMouseClickedCoord();
                if (clickedCoord != null)
                {
                    friendlyPos[_countAssigned] = clickedCoord;
                    _countAssigned += 1;
                }
            }

            else if (_countAssigned == 5)
            {
                Debug.Log("Done assigning coord, counted: " + _countAssigned);
                IsAssigned = true;
                done = true;
            }

            // waiting for mouse click
            yield return null;
        }

    }



}
