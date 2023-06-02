using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimationEvent : MonoBehaviour, IEventHundle
{
    public event IEventHundle.EventHundle _event;

    public void Execute()
    {
        gameObject.SetActive(false);
        _event?.Invoke();
    }
}
