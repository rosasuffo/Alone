using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCBaseState
{
    public abstract void EnterState(NPC npc);
    public abstract void UpdateState(NPC npc);
}

public class NPCIdleState : NPCBaseState
{
    public override void EnterState(NPC npc)
    {
        Debug.Log("NPC idle");
    }
    public override void UpdateState(NPC npc)
    {
        if (npc.seen)
        {
            if(npc.CompareTag("npc1")) npc.SwitchState(npc.dyingState);
            else npc.SwitchState(npc.hidingState);
        } 
    }
}

public class NPCDyingState : NPCBaseState
{
    public override void EnterState(NPC npc)
    {
        Debug.Log("NPC dying");
    }
    public override void UpdateState(NPC npc)
    {
        if (npc.rb.velocity.y > -1)
        {        
            npc.rb.AddForce(npc.transform.forward * npc.force);
        }
        else
        {
            npc.SwitchState(npc.deadState);
        }
    }
}
public class NPCDeadState : NPCBaseState
{
    public override void EnterState(NPC npc)
    {
        Debug.Log("NPC dead");
        npc.rb.constraints = RigidbodyConstraints.None;
    }
    public override void UpdateState(NPC npc)
    {
        if(npc.transform.position.y < 0.5 && npc.rb.constraints == RigidbodyConstraints.None)
        {
            npc.Wait(3);
        }
    }
}

public class NPCHidingState : NPCBaseState
{
    public override void EnterState(NPC npc)
    {
        Debug.Log("NPC hiding");
        npc.seen = false;
    }
    public override void UpdateState(NPC npc)
    {
        if(npc.transform.position.x < 210)
        {
            Vector3 force = -1 * npc.force * npc.transform.forward;
            npc.rb.AddForce(force);
        }
        else if (npc.seen) 
        { 
            npc.SwitchState(npc.dyingState); 
        }
    }
}   

