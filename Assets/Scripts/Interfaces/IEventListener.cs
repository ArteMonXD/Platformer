using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventListener
{
    public abstract IEventHundle EventHundle { get; set; }
    public abstract void ListenerField();
}
