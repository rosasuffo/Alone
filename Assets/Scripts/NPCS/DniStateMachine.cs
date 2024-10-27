using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DniBaseState
{
    public abstract void EnterState(Dni dni);
    public abstract void UpdateState(Dni dni);
}
public class DniPickedState : DniBaseState
{
    public override void EnterState(Dni dni)
    {
        Debug.Log("dni picked");
    }
    public override void UpdateState(Dni dni)
    {
        Subject subject = dni.GetSubject();
        dni.transform.position = subject.transform.position + subject.transform.forward;
        dni.transform.eulerAngles = new Vector3(subject.transform.eulerAngles.x-90,subject.transform.eulerAngles.y, subject.transform.eulerAngles.z);

        if (!dni.picked) dni.SwitchState(dni.unpickedState);
    }
}

public class DniUnpickedState : DniBaseState
{
    public override void EnterState(Dni dni)
    {
        Debug.Log("dni unpicked");
    }
    public override void UpdateState(Dni dni)
    {        
        dni.transform.localPosition = dni.initialLocalPos;
        if (dni.picked) dni.SwitchState(dni.pickedState);
    }
}