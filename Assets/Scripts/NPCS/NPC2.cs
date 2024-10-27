using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2 : NPC
{
    public override void OnNotify(PlayerActions action)
    {
        switch (action)
        {
            case PlayerActions.Npc2Seen:
                seen = true;
                return;
            default:
                return;
        }
    }
}
