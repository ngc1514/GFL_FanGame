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
    - size is hardcoded to 5


************************************************************************************/

public class Stage
{
    public bool IsCoordAssigned { get; private set; } = false; // by default is false
    private int _countAssigned = 0;


    public string Name { get; set; }
    public float Length { get; set; } //FIXME: need to re-adjust plane collider size?
    public float Width { get; set; }
    public float ZBound
    {
        get
        {
            return 0.5f * Width;
        }
    }

    public List<Enemy> enemyList;
    public List<Coord> blocks;
    public List<Coord> enemyPos;


    public bool IsEcheChosen { get; private set; } = false; // by default is false
    public Echelon chosenEchelon;
    public int chosenEchelonIdx = -1;
    public List<Coord> friendlyPos = new List<Coord>();


    //public IEnumerator ChooseEchelon()
    //{
    //    if (chosenEchelon is null)
    //    {
    //        yield return WaitForChooseEchelon();
    //    }
    //    else
    //    {
    //        Debug.LogError("You shouldn't have chosen an echelon already.");
    //        yield return null;
    //    }
    //}

    public IEnumerator WaitForChooseEchelon()
    {
        bool done = false;
        while (!done)
        {
            if (Input.GetMouseButtonDown(1)) // TODO: need to detect GUI button click 
            {
                chosenEchelonIdx = 0; // FIXME: this is for testing. need GUI
                chosenEchelon = PlayerData.echelonList[chosenEchelonIdx];
            }

            else if (chosenEchelonIdx != -1 && chosenEchelonIdx < PlayerData.MaxEchelonSlot && chosenEchelon != null)
            {
                IsEcheChosen = true;
                Debug.Log("Done choosing echelon. Chosen echlon index: " + chosenEchelonIdx);
                done = true;
            }

            // waiting for choosing echelon
            Debug.Log("Waiting for choosing");
            yield return null;
        }

    }


    // TODO: do i need this. do i need more in this?
    public bool IsEveryoneAssigned()
    {
        if (chosenEchelonIdx != -1 && PlayerData.echelonList[chosenEchelonIdx].Size > 0)
        {
            for (int i = 0; i < chosenEchelon.Size; i++)
            {
                if (friendlyPos[i] != null) //FIXME: out of range i
                {
                    IsCoordAssigned = true;
                }
                else
                {
                    IsCoordAssigned = false;
                    return IsCoordAssigned;
                }
            }
        }
        else
        {
            IsCoordAssigned = false;
            Debug.LogError("Something is wrong with the chosen echelon");
        }
        return IsCoordAssigned;
    }



    public IEnumerator WaitForAllCoordBeAssigned(Stage stage)
    {
        bool done = false;
        while (!done)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Assigning: " + countAssigned);
                ControlManager ctrlManager = GameObject.FindGameObjectWithTag("ControlManager").GetComponent<ControlManager>();

                Coord clickedCoord = ctrlManager.GetMouseClickedCoord(stage);
                if (clickedCoord != null)
                {
                    friendlyPos.Add(clickedCoord);
                    _countAssigned += 1;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.LogWarning("Re-assigning member coord");
                friendlyPos.RemoveAt(friendlyPos.Count - 1);
                _countAssigned -= 1;
            }

            else if (_countAssigned == PlayerData.echelonList[chosenEchelonIdx].Size && IsEveryoneAssigned())
            {
                Debug.Log("Done assigning coord, counted: " + _countAssigned);
                IsCoordAssigned = true;
                done = true;
            }

            // waiting for mouse click
            Debug.Log("Waiting for assigning coord");
            yield return null;
        }

    }



}
