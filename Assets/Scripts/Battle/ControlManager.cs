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
    private Plane _Plane;
    private GameObject _currentSelectedObj;
    #endregion


    void Awake()
    {
        EnsureSingleton();
    }


    void Start()
    {
        _Plane = new Plane(Vector3.up, 0);
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


    // Returning Coord that player clicks
    // TODO: need spacing at least 0.7 between each member
    // TODO: add animation responce when clicking
    public Coord GetMouseClickedCoord(Stage stage)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // for hitting a GameObject
            RaycastHit hitInfo = new RaycastHit();
            bool hitObject = Physics.Raycast(ray, out hitInfo);
            _currentSelectedObj = hitInfo.transform.gameObject;
            Vector3 hitObjPos = _currentSelectedObj.transform.position;

            // for hitting plane
            bool hitPlane = _Plane.Raycast(ray, out float rayToPlaneDistance);
            Vector3 hitPlanePoint = ray.GetPoint(rayToPlaneDistance);


            if (hitObject && (hitPlanePoint.z < stage.ZBound || hitObjPos.z < stage.ZBound))
            {          
                Transform parent = _currentSelectedObj.transform.parent;

                // if clicked ground, go to that exact coord
                if (parent.name == "ground")
                {
                    Debug.Log("hit plane: " + hitPlanePoint);
                    return new Coord((float)Math.Round(hitPlanePoint.x, 1), 0, (float)Math.Round(hitPlanePoint.z, 1));
                }

                // if hit cube, go behind the cube -0.8 z offset
                // TODO: validate it's actually a block not people 
                // TODO: in case if in the future there are more than one echelon on the field
                else
                {
                    Debug.Log(string.Format("hit object: {0}, ({1}, {2}, {3})",
                                                parent.name,
                                                hitObjPos.x, hitObjPos.y, hitObjPos.z));
                    return new Coord(hitObjPos.x, 0, hitObjPos.z - 0.8f); ;
                }                
            }
            else
            {
                Debug.LogError("Not hitting anything or coord is above 1/2 width.");
                return null;
            }
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
