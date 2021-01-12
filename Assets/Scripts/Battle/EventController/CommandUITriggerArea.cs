using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// FIXME: i dont think i need this rn
public class CommandUITriggerArea : MonoBehaviour
{
    private void TriggerCommandUIShow(Collider other)
    {
        BattleEvent.current.CommandUITriggerClick();
    }
}
