using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISuperAttack
{
    public abstract float Charge { get; }
    public abstract float MaxCharge { get; }
    public abstract float SuperAttackPrice { get; }

    public abstract void SuperAttackInit();
    public abstract void SuperAttackDo();

    public abstract void UpCharge(float value);
}
