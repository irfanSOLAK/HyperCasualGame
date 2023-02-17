using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


/// <summary>
/// Any class that listen an event must derived from Listener. It must have methods same name with the listened event. 
/// (e.g. If a class listen Countdown event it must have a method named Countdown as public. When the event triggered Countdown method is called.)
/// </summary>
public abstract class Listener : MonoBehaviour, IListener
{
    public abstract void AddEventListeners();
    public abstract void RemoveEventListeners();

    public virtual void OnEnable()
    {
        AddEventListeners();
    }

    public virtual void OnDisable()
    {
        RemoveEventListeners();
    }

    public void OnEventOccured(NotificationManager.EVENT_TYPE eventName, float parameter = 0)
    {
        Type thisType = this.GetType();
        MethodInfo theMethod = thisType.GetMethod(eventName.ToString());
        theMethod.Invoke(this, new object[] { parameter });
    }
}