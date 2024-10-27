using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dni : MonoBehaviour, IObserver
{
    [SerializeField] Subject _playerSubject;
    public bool picked = false;
    public Vector3 initialLocalPos;

    DniBaseState currentState;
    public DniPickedState pickedState = new DniPickedState();
    public DniUnpickedState unpickedState = new DniUnpickedState();
    void Start()
    {
        initialLocalPos = transform.localPosition;
        currentState = unpickedState;
        currentState.EnterState(this);
    }
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(DniBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void OnNotify(PlayerActions action)
    {
        switch (action)
        {
            case(PlayerActions.DniInteraction):
                Debug.Log("interaction with dni");
                picked = !picked;
                return;
            default:
                return;
        }
    }
    private void OnEnable()
    {
        _playerSubject.AddObserver(this);
    }

    // TRIGGER
    private void OnTriggerStay(Collider other)
    {
        if (ReferenceEquals(other, _playerSubject.GetComponent<Collider>()))
        {
            if (currentState == pickedState)
            {
                _playerSubject.NotifyObservers(PlayerActions.CantInteract);
            }
            else
            {
                _playerSubject.NotifyObservers(PlayerActions.CanInteract);
            }
            if (Input.GetKeyDown(KeyCode.E)) _playerSubject.NotifyObservers(PlayerActions.DniInteraction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (ReferenceEquals(other, GetSubject().GetComponent<Collider>()))
        {
            GetSubject().NotifyObservers(PlayerActions.CantInteract);
        }
    }

    // GETTERS
    public Subject GetSubject()
    {
        return _playerSubject;
    }
}
