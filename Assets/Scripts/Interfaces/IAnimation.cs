using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimation
{
    public abstract Animator Animator { get; }
    public abstract void AnimationStatusControl();
}
