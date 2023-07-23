using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelInactive : EventListener<EndAnimationEvent>
{
    public override IEventHundle EventHundle
    {
        get
        {
            eventHundle = eventHundleObject;
            return eventHundle;
        }
        set => eventHundle = value;
    }

    protected override void EventListen()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        EventHundle._event += EventListen;
    }
}
