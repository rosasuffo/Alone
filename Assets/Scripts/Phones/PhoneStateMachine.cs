using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

//  - si est� sonando, deja de sonar y empieza di�logo.
//  - cuando termina el dialogo, lo tiene en la mano y si interacciona lo deja dnd est� m�s cerca (mesa, suelo)
//  - si lo ha dejado e interacciona con �l, lo coge.
//  - cuando llega a la habit de luis y est� sonando, lo coge y segundo di�logo.

public abstract class PhoneBaseState
{
    public abstract void EnterState(Phone phone);
    public abstract void UpdateState(Phone phone);
}

public class PhoneUnpickedState : PhoneBaseState //DISABLED
{
    public override void EnterState(Phone phone) 
    {
        phone.rb.constraints = RigidbodyConstraints.None;
        Debug.Log("Phone on unpicked state");
        phone.rb.AddForce(phone.transform.up * 1000);

    }
    public override void UpdateState(Phone phone)
    {
        if (phone.calling) phone.SwitchState(phone.ringingState);
        else if (phone.picked) phone.SwitchState(phone.pickedState);
    }
}
public class PhonePickedState : PhoneBaseState
{
    public override void EnterState(Phone phone)
    {
        phone.rb.constraints = RigidbodyConstraints.FreezeRotation;
        Debug.Log("Phone on picked state");
    }
    public override void UpdateState(Phone phone) 
    {
        Subject subject = phone.GetSubject();
        phone.transform.position = subject.transform.position + (subject.transform.forward / 2) + (subject.transform.right / 2);

        if (phone.calling) phone.SwitchState(phone.ringingState);
        else if (!phone.picked)
        {
            phone.SwitchState(phone.unpickedState);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            phone.picked = false;
            phone.SwitchState(phone.unpickedState);
        }
    }
}
public class PhoneRingingState : PhoneBaseState
{
    public override void EnterState(Phone phone)
    {
        Debug.Log("Phone on ringing state");
        phone.GetAudioSource().Play();
    }
    public override void UpdateState(Phone phone) 
    {
        if(phone.picked && phone.whatCall == Phone.call.one) phone.SwitchState(phone.dialogue1State);
        else if (phone.picked && phone.whatCall == Phone.call.two) phone.SwitchState(phone.dialogue2State);

    }
}

public class PhoneDialogue1State : PhoneBaseState
{
    public override void EnterState(Phone phone)
    {
        phone.GetAudioSource().Pause();
        phone.dialogue = true;

        phone.rb.constraints = RigidbodyConstraints.FreezeRotation;

        Debug.Log("Phone on dialogue1 state");
    }
    public override void UpdateState(Phone phone) 
    {
        // dialogue
        Subject subject = phone.GetSubject();
        phone.transform.position = subject.transform.position + (subject.transform.forward / 2) + (subject.transform.right / 2);

        if (!phone.dialogue) phone.SwitchState(phone.pickedState);
    }
}
public class PhoneDialogue2State : PhoneBaseState
{
    public override void EnterState(Phone phone)
    {
        phone.GetAudioSource().Pause();
        phone.dialogue = true;

        phone.rb.constraints = RigidbodyConstraints.FreezeRotation;

        Debug.Log("Phone on dialogue2 state");
    }
    public override void UpdateState(Phone phone)
    {
        // dialogue
        Subject subject = phone.GetSubject();
        phone.transform.position = subject.transform.position + (subject.transform.forward / 2) + (subject.transform.right / 2);

        if (!phone.dialogue) phone.SwitchState(phone.pickedState);
    }
}