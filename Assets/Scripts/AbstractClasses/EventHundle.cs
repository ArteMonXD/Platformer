using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventHundle : MonoBehaviour, IEventHundle
{
    public event IEventHundle.EventHundle _event;

    public virtual void Execute()
    {
        _event?.Invoke();
    }
}
