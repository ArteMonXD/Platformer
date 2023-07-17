using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    public abstract Rigidbody2D m_RigidBody { get; }
    public abstract float JumpForce { get; }
    public abstract float SpeedWalk { get; }
    public abstract bool IsGround { get; }
    public abstract void Movement(float horizontalInput, bool jumpInput);
    public abstract void Move();
    public abstract void Jump();
    public abstract void CheckGround();
}
