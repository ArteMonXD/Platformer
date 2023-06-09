using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimationEvent : EventHundle
{
    public override void Execute()
    {
        gameObject.SetActive(false);
        base.Execute();
    }
}
