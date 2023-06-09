using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitDamageDealer : MonoBehaviour, IDamageDealer
{
    public static int damageID = 0;
    protected IAttack owner;
    public IAttack DamageOwner { get { return owner; }}
    protected float damage;
    public float Damage { get { return damage; }}
    protected bool isAttack;
    public bool IsAttack { get { return isAttack; } }

    private void Awake()
    {
        damageID++;
    }
    public virtual void MakeDamage(IHealthAndDamage DamageRecipient, float damageValue)
    {
       (DamageRecipient as Unit).Damage(damageValue);
    }
    public virtual void SetAttack(float damageValue, IAttack setOwner)
    {
        damage = damageValue;
        owner = setOwner;
        isAttack = true;
    }
    public virtual void SetAttack(float damageValue)
    {
        damage = damageValue;
        isAttack = true;
    }
}
