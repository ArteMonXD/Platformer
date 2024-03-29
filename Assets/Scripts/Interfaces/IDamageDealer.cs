using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDealer
{
    public abstract IAttack DamageOwner { get;}
    public abstract float Damage { get;}
    public abstract bool IsAttack { get;}
    public abstract bool MakeDamage(IHealthAndDamage DamageRecipient, float damageValue);
    public abstract void SetAttack(float damageValue, IAttack owner);

    public abstract void SetAttack(float damageValue);

    public abstract bool CheckVictim(GameObject possibleVictim, ref IHealthAndDamage damageRecipient);
}
