using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IObserver
{
    [SerializeField] Subject _playerSubject;
    public Rigidbody rb;
    public float force; // applied for movement

    NPCBaseState currentState;
    public NPCIdleState idleState = new NPCIdleState();
    public NPCDyingState dyingState = new NPCDyingState();
    public NPCDeadState deadState = new NPCDeadState();
    public NPCHidingState hidingState = new NPCHidingState();

    public bool seen;
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
        rb = GetComponent<Rigidbody>();
        seen = false;
    }
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(NPCBaseState state)
    {
        currentState=state;
        state.EnterState(this);
    }

    public abstract void OnNotify(PlayerActions action);

    private void OnEnable()
    {
        _playerSubject.AddObserver(this);
    }
    public Subject GetSubject()
    {
        return _playerSubject;
    }

    // COROUTINE
    public void Wait(int seconds)
    {
        StartCoroutine(WaitAndFreeze(seconds));
    }

    IEnumerator WaitAndFreeze(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        rb.constraints = RigidbodyConstraints.FreezeAll;

    }
}
