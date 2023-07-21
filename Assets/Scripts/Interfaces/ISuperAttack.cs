using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISuperAttack
{
    public abstract byte SuperAttackVariantCount { get; }
    public abstract float[] DamageSuperAttack { get; }
    public abstract int SuperAttackCounter { get; }
    public abstract float Charge { get; }
    public abstract float MaxCharge { get; }
    public abstract float SuperAttackPrice { get; }
    public abstract bool IsSuperAttacking { get; }
    public abstract void SuperAttackInput(bool superAttackInput);
    public abstract void SuperAttackInit();
    public abstract void SuperAttackDo(); 
    public abstract void UpCharge(float value);
}
