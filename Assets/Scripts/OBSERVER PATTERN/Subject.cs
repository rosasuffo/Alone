using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// every class that inherits this one will be able to comunicate its observers whatever happens
public abstract class Subject : MonoBehaviour
{
    // collection of all the observers of THIS subject
    private List<IObserver> _observers = new List<IObserver>();

    // add a new observer to the subject's collection
    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    // remove an observer from the subject's collection
    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    // notify each observer that an event has occured
    public void NotifyObservers(PlayerActions action)
    {
        _observers.ForEach((_observer) =>
        {
            _observer.OnNotify(action);
        });
    }
}
