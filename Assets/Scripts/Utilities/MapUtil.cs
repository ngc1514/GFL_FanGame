using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************************************

This script is responsible for 

- Map json file contains the coordinates for blocks (and enemies?) - friendly location determines by echelon module

- Validate map json files 
    - Size
    - There need to be at least one opening between enemies and friendly.

- Validate enemy spawning location
    - all enemies need to spawn above 1/2 * width


- //TODO: Generating json map files for each stage ??
- //TODO: Map creation tool ??

************************************************************************************/

public class MapUtil : MonoBehaviour
{
    // BFS/DFS? See if boundary connects from one side to another?
    bool ValidateMap()
    {
        return false;
    }


    //void GenerateMapJson()
    //{

    //}
}
