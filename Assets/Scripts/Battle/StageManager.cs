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
    [SerializeField] private GameObject _caratPrefab;


    Stage testStage = new Stage();


    void Awake()
    {
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
            new Coord(1, 0, 1),
            new Coord(3, 0, 4)
        };
        testStage.blocks = blocks;

        Friendly testFriendly = new Friendly();
        testFriendly.EchIdx = 0;
        testFriendly.PosIdx = 0;
        testFriendly.Name = "ump9";
        testFriendly.multc = 1;
        testFriendly.prefabObj = _friendlyPrefab;


        Echelon testEchelon0 = new Echelon();
        testEchelon0.AddMemeber(testFriendly);
        testEchelon0.AddMemeber(testFriendly);
        testEchelon0.AddMemeber(testFriendly);


        PlayerData.echelonList.Add(testEchelon0);
        

    }



    void Start()
    {
        if (!testStage.IsEcheChosen)
        {
            Debug.Log("Choose your echelon");
            StartCoroutine(AskToChooseEchAndAssignCoord(testStage));
        }

    }



    private IEnumerator AskToChooseEchAndAssignCoord(Stage stage)
    {
        yield return stage.WaitForChooseEchelon();

        yield return stage.WaitForAllCoordBeAssigned(stage);


        // TODO: ask player to confirm coord, need GUI
        for (int i = 0; i < PlayerData.echelonList[0].Size; i++)
        {
            GameObject carat = Instantiate(_caratPrefab, testStage.friendlyPos[i].GetVector3(), Quaternion.identity); ;
        }

        // TODO: after assigning, spawn friendly at corresponding coord

    }



}
