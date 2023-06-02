using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventListener<T> : MonoBehaviour
{
    protected IEventHundle eventHundle;
    public abstract IEventHundle EventHundle { get; set; }
    public T eventHundleObject;
    protected abstract void EventListen();

}
