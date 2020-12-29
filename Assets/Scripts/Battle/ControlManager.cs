using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************************************

This script is responsible for 

- Game controls
- ex: character selection and command assignment 

// TODO: if selecting box: move -0.8 behind. If not, move to exact coord

************************************************************************************/

public class ControlManager : MonoBehaviour
{
    #region Singleton
    private static ControlManager _instance;
    public static ControlManager Instance
    {
        get
        {
            if (!_instance)
            {
                Debug.LogWarning("An instance of ControlManager is needed. Creating one now...");
                _instance = new GameObject("ControlManager", typeof(ControlManager)).GetComponent<ControlManager>();
            }

            return _instance;
        }
    }
    #endregion


    #region Variables
    [SerializeField] private GameObject _friendlyPrefab;
    Plane m_Plane;
    private GameObject _currentSelectedObj;
    #endregion


    private void Awake()
    {

    }


    void Start()
    {
        m_Plane = new Plane(Vector3.up, 0);
        //Debug.Log("plane pos: " + m_Plane.normal.x + ", " + m_Plane.normal.y + ", " + m_Plane.normal.z);
    }


    void Update()
    {

    }


    // Ensure only one ControlManager exists in a scene
    private void EnsureSingleton()
    {
        if (!_instance)
            _instance = this;

        var controlManagerInstances = FindObjectsOfType<ControlManager>();

        if(controlManagerInstances.Length != 1)
        {
            Debug.LogError("Only one instance of ControlManager is allowed to exist. Please remove redundant ControlManager");

            // if found more than one ControlManager, destory them
            for(int i=0; i<controlManagerInstances.Length; i++)
            {
                ControlManager otherInstance = controlManagerInstances[i];
                if(otherInstance != Instance)
                {
                    if (otherInstance.gameObject == Instance.gameObject)
                        Destroy(otherInstance);
                    else
                        Destroy(otherInstance.gameObject);
                }     
            }
        }
    }


    public Coord GetMouseClickedCoord()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hitObject = Physics.Raycast(ray, out hitInfo);

            float rayToPlaneDistance = 0.0f;
            bool hitPlane = m_Plane.Raycast(ray, out rayToPlaneDistance);

            if (hitObject)
            {
                _currentSelectedObj = hitInfo.transform.gameObject;

                // if clicked ground, go to that exact coord
                if (_currentSelectedObj.transform.parent.name == "ground")
                {
                    Vector3 hitPoint = ray.GetPoint(rayToPlaneDistance);
                    Debug.Log("hitGound: " + hitPoint);
                    return new Coord((float)Math.Round(hitPoint.x, 1), (float)Math.Round(hitPoint.z, 1));
                }

                // if hit cube, go behind the cube -0.8 z offset
                // FIXME: need to validate it's actually block not enemy (or friendly)
                else
                {
                    Vector3 pos = _currentSelectedObj.transform.position;
                    Debug.Log(string.Format("hit: {0}, ({1}, {2}, {3})",
                                                _currentSelectedObj.transform.parent.name,
                                                pos.x, pos.y, pos.z));
                    return new Coord(pos.x, pos.z - 0.8f); ;
                }
            }
            else
                return null;
        }
        else
            return null;
    }


    // TODO:
    public void MoveFriendly()
    {
        //_friendlyPrefab.transform.position = hitPoint;
    }
}
