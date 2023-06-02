using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventHundle
{
    public delegate void EventHundle();
    public event EventHundle _event;

    public abstract void Execute();
}
