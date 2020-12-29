using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************************************

This script is responsible for 

- Read map files
- Generate current stage
- Spawning enemies
- Ask player to assign friendly coord

************************************************************************************/

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject _block;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _friendlyPrefab;

    Stage testStage = new Stage();


    void Awake()
    {
        // TODO: null checking for block
        //float enePosX = _enemyBlock.transform.position.x;
        //float enePosY = _enemyBlock.transform.position.y;
        //float enePosZ = _enemyBlock.transform.position.z;
        //Vector3 enemySpawnPos = new Vector3(enePosX, enePosY - 0.5f, enePosZ + 0.8f);

        //float EneRotaX = _enemyBlock.transform.rotation.x;
        //float EneRotaY = _enemyBlock.transform.rotation.y;
        //float EneRotaZ = _enemyBlock.transform.rotation.z;
        //GameObject spawnedEnemy = Instantiate(_enemyPrefab, enemySpawnPos, Quaternion.Euler(new Vector3(EneRotaX, EneRotaY + 180, EneRotaZ)));


        //float friPosX = _friendlyBlock.transform.position.x;
        //float friPosY = _friendlyBlock.transform.position.y;
        //float friPosZ = _friendlyBlock.transform.position.z;
        //Vector3 friendlySpawnPos = new Vector3(friPosX, friPosY - 0.5f, friPosZ - 0.8f);
        //GameObject spawnedFriendly = Instantiate(_friendlyPrefab, friendlySpawnPos, _friendlyBlock.transform.rotation);

        testStage.Name = "Test Stage";
        testStage.Length = 10;
        testStage.Width = 10;
        List<Coord> blocks = new List<Coord>
        {
            new Coord(1, 1),
            new Coord(3, 4)
        };
        testStage.blocks = blocks;
    }

    void Start()
    {
        // start assigning echelon location
        if (!testStage.IsEveryoneAssigned())
        {
            Debug.Log("Start assigning coord");
            StartCoroutine(AskToAssign(testStage));
        }
    }


    void Update()
    {
        // Assign friendly pos when friendly pos list is empty
        // click anywhere on map before 1/2 width to assign friendly coord
        // if click on block, -0.8 Z offset
    }


    private IEnumerator AskToAssign(Stage stage)
    {
        // wait for coord assignment
        yield return stage.AssignEchePos();

        // TODO: record all to be assigned and save to array
        yield return WaitForEcheAssign(stage);
    }

    private IEnumerator WaitForEcheAssign(Stage stage)
    {
        bool done = false;

        while (!done)
        {
            if (stage.IsAssigned)
            {
                done = true;
            }
            yield return null;
        }
    }

}
