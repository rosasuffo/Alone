using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableIcon : MonoBehaviour, IObserver
{
    [SerializeField] Subject _playerSubject;
    private Image icon;

    void Start()
    {
        icon = GetComponent<Image>();
        icon.enabled = false;
    }

    public void OnNotify(PlayerActions action)
    {
        if(action == PlayerActions.CanInteract)
        {
            icon.enabled = true;
        }
        else if (action == PlayerActions.CantInteract)
        {
            icon.enabled = false;
        }
    }
    private void OnEnable()
    {
        _playerSubject.AddObserver(this);
    }
}
