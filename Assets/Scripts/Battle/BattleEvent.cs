using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************************************

Battle event handlers

- displaying command UI when clicking on a friendly unit

- // TODO: need to finish echelon init and shit 

************************************************************************************/



public class BattleEvent : MonoBehaviour
{
    public static BattleEvent current;

    public event EventHandler OnFriendlyClicked;

    private void Awake()
    {
        current = this;    
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        CommandUITriggerClick();
    }


    // FIXME: only check of detect after player deployed an echelon

    public void CommandUITriggerClick()
    {
        if (OnFriendlyClicked != null && IsClickingFriendly())
        {
            OnFriendlyClicked.Invoke(this, EventArgs.Empty);
        }
    }



    public bool IsClickingFriendly()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            GameObject hitColliderObj;

            // for hitting a GameObject
            RaycastHit hitInfo = new RaycastHit();
            bool hitObject = Physics.Raycast(ray, out hitInfo);
            if(hitInfo.transform != null)
            {
                hitColliderObj = hitInfo.transform.gameObject;
                Vector3 hitObjPos = hitColliderObj.transform.position;

                if (hitObject)
                {
                    Transform hitColliderParent = hitColliderObj.transform.parent;

                    if (hitColliderParent != null && IsFriendlyType(hitColliderParent)) //hitColliderParent.name == "friendly") 
                    {
                        Debug.Log(string.Format("Selecting friendly {0}", hitColliderParent.GetComponent<Friendly>().unitID));

                        return true;
                    }
                }
            }
        }
        return false;
    }


    // FIXME: need to check Unit ID so that not every command UI will show
    public bool IsFriendlyType(Transform parent)
    {
        if(parent.GetComponent<Friendly>() != null)
        {
            Debug.LogWarning("It's friendly type");
            return true;
        }
        else
        {
            return false;
        }
    }


}
