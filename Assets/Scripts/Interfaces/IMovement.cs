using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    public abstract Rigidbody2D m_RigidBody { get; }
    public abstract float JumpForce { get; }
    public abstract AnimationCurve SpeedWalk { get; }
    public abstract bool IsGround { get; }
    public abstract bool IsCrouch { get; }
    public abstract float JumpOffset { get; }
    public abstract Transform FootCollider { get; }
    public abstract LayerMask GroundLayers { get; }
    public abstract void Movement(float horizontalInput, bool jumpInput);
    public abstract void Move(float inputValue);
    public abstract void Jump();
    public abstract void CheckGround();
}
