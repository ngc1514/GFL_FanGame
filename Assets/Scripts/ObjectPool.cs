using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Singleton
    private static ObjectPool _instance;
    public static ObjectPool Instance { get { return _instance; } }
    #endregion

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private void Awake()
    {
        #region Singleton Awake
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion
    }


    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        tmp = Instantiate(objectToPool);
        tmp.SetActive(false);
        pooledObjects.Add(tmp);
    }

}
