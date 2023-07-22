using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    public abstract IDamageDealer Dealer { get; }
    public abstract byte AttackVariantCount { get; }
    public abstract byte CrouchAttackVariantCount { get; }
    public abstract byte FlyAttackVariantCount { get; }
    public abstract float[] DamageAttack { get; }
    public abstract float[] DamageCrouchAttack { get; }
    public abstract float[] DamageFlyAttack { get; }
    public abstract int AttackCounter { get; }
    public abstract int CrouchAttackCounter { get; }
    public abstract int FlyAttackCounter { get; }
    public abstract Coroutine TimerAttackReset { get; }
    public abstract Coroutine TimerCrouchAttackReset { get; }
    public abstract Coroutine TimerFlyAttackReset { get; }
    public abstract float ResetTime { get; }
    public abstract bool IsAttacking { get; }
    public abstract void AttackInput(bool attackInput);
    public abstract void AttackInit();
    public abstract void AttackDo();
    public abstract void CrouchAttackInit();
    public abstract void CrouchAttackDo();
    public abstract void FlyAttackInit();
    public abstract void FlyAttackDo();
    public abstract IEnumerator AttackReset();
    public abstract IEnumerator FlyAttackReset();
    public abstract IEnumerator CrouchAttackReset();
}
