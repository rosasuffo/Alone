using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Phone : MonoBehaviour, IObserver
{
    [SerializeField] Subject _playerSubject;
    public Rigidbody rb;
    public bool calling = false;
    public enum call { one, two }
    public call whatCall;
    public bool dialogue = false;
    public bool picked = false;
    private AudioSource ring_sound;

    // FSM
    PhoneBaseState currentState;
    public PhoneUnpickedState unpickedState = new PhoneUnpickedState();
    public PhonePickedState pickedState = new PhonePickedState();
    public PhoneRingingState ringingState = new PhoneRingingState();
    public PhoneDialogue1State dialogue1State = new PhoneDialogue1State();
    public PhoneDialogue2State dialogue2State = new PhoneDialogue2State();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ring_sound = GetComponent<AudioSource>();
        currentState = unpickedState;
        currentState.EnterState(this);
    }   
    void Update()
    {
        currentState.UpdateState(this);
    } 

    public void SwitchState(PhoneBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    public abstract void OnNotify(PlayerActions action);
    private void OnEnable()
    {
        _playerSubject.AddObserver(this);
    }

    // GETTERS
    public Subject GetSubject()
    {
        return _playerSubject;
    }
    public PhoneBaseState GetCurrentState()
    {
        return currentState;
    }
    public AudioSource GetAudioSource()
    {
        return ring_sound;
    }
}