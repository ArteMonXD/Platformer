using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    public abstract IDamageDealer Dealer { get; }
    public abstract byte AttackVariantCount { get; }
    public abstract float[] DamageAttack { get; }
    public abstract float this[int index] { get;}
    public abstract Animator Animator { get; }
    public abstract int AttackCounter { get; }
    public abstract bool IsAttacking { get; }

    public abstract void AttackInit();
    public abstract void AttackDo();
}
