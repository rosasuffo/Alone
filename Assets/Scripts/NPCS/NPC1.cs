using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : NPC
{
    public override void OnNotify(PlayerActions action)
    {
        switch (action)
        {
            case PlayerActions.Npc1Seen:
                seen = true;
                return;
            default:
                return;
        }
    }
}
