using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echelon
{
    public int EchIdx { get; set; } = 0;


    private List<Friendly> _members = new List<Friendly>();


    public int Size
    {
        get
        {
            return _members.Count;
        }
    }


    public void AddMemeber(Friendly friend)
    {
        if(Size >= 0 && Size <= 5)
        {
            _members.Add(friend);
        }
        else
        {
            Debug.LogError("An echelon can only have <= 5 members");
        }
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

}
