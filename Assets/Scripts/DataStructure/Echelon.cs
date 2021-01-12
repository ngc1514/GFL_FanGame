using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************************************

Holds echelon data

************************************************************************************/

public class Echelon
{
    // singleton
    public static Echelon currentEchelon;


    #region Variables
    // Fixme: when a new echelon is created, it's index should be (current ech count + 1) < echelon slots
    private int echIdx;
    private List<Friendly> _members = new List<Friendly>();
    public int Size
    {
        get
        {
            return _members.Count;
        }
    }
    #endregion

    //private void Awake()
    //{
    //    currentEchelon = this;
    //}


    /*
     * when trying to create an echelon, 
     * constructor will check if there's any free slot left
     * if NOT, no echelon will be added to PlayerData.echelonList
     */
    public Echelon()
    {
        if (PlayerData.GetEchelonCount() < PlayerPrefs.GetInt("MaxEchelonSlot"))
        {
            echIdx = PlayerData.GetEchelonCount();
            PlayerData.echelonList.Add(this);
        }
        else
        {
            Debug.LogError("No more slot to add echelon!");
            currentEchelon = null;
        }
    }


    // =============================== Helper methods =============================== //

    /*
     * when adding a member to an echelon, 
     * Automatically set:
     *      echIdx
     *      posIdx
     */
    public void AddMemeber(Friendly friend)
    {
        if(Size >= 0 && Size < 5)
        {
            friend.SetEchIdx(echIdx);
            friend.SetPosIdxForAddedMember(Size);
            AddToEchelon(friend);
        }
        else
        {
            Debug.LogError("An echelon can only have <= 5 members");
        }
    }

    // TODO: remove member function
    public void RemoveMemberAtIndex(int indexToRemove)
    {

    }


    public List<Friendly> GetMemberList()
    {
        return _members;
    }

    public Friendly GetMemberAtIndex(int idx)
    {
        if(idx > 0 && idx < _members.Count)
        {
            return _members[idx];
        }
        else
        {
            Debug.LogError("Member index out of bound!");
            return null;
        }
    }

    public int GetEchIndex()
    {
        return echIdx;
    }

    public void SetEchIndex(int newIndex)
    {
        echIdx = newIndex;
    }

    public void AddToEchelon(Friendly unitToAdd)
    {
        if(Size < 5)
        {
            unitToAdd.SetEchIdx(echIdx);
            _members.Add(unitToAdd);
        }
    }

}
