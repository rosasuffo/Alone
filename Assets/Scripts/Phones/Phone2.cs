using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone2 : Phone
{
    public override void OnNotify(PlayerActions action)
    {
        switch (action)
        {
            case (PlayerActions.Phone2Reached):
                calling = true;
                whatCall = call.one;
                return;

            case (PlayerActions.Phone2Interaction):
                if (calling) // si estan llamando, empieza dialogo
                {
                    picked = true;
                    calling = false;
                }
                else // si no, picked/unpicked
                {
                    picked = !picked;
                }
                return;

            case (PlayerActions.EnterRoom):
                if (picked)
                {
                    calling = true;
                    whatCall = call.two;
                }
                return;

            default:
                return;
        }
    }
    // TRIGGER
    private void OnTriggerStay(Collider other)
    {
        if (ReferenceEquals(other, GetSubject().GetComponent<Collider>()))
        {
            if (GetCurrentState() == pickedState || GetCurrentState() == dialogue1State || GetCurrentState() == dialogue2State)
            {
                GetSubject().NotifyObservers(PlayerActions.CantInteract);
            }
            else
            {
                GetSubject().NotifyObservers(PlayerActions.CanInteract);
            }
            if (Input.GetKeyDown(KeyCode.E)) GetSubject().NotifyObservers(PlayerActions.Phone2Interaction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (ReferenceEquals(other, GetSubject().GetComponent<Collider>()))
        {
            GetSubject().NotifyObservers(PlayerActions.CantInteract);
        }
    }
}
