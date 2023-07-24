using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitDamageDealer : MonoBehaviour, IDamageDealer
{
    [SerializeField] protected IAttack owner;
    public IAttack DamageOwner { get { return owner; }}
    protected float damage;
    public float Damage { get { return damage; }}
    protected bool isAttack;
    public bool IsAttack { get { return isAttack; } }
    public virtual bool MakeDamage(IHealthAndDamage DamageRecipient, float damageValue)
    {
        //Debug.Log((DamageRecipient as Unit).gameObject.name + "/" + (owner as Unit).gameObject.name);
        if (!DamageRecipient.Equals(owner))
        {
            //Debug.Log((DamageRecipient as Unit).gameObject.name + "/" + (owner as Unit).gameObject.name + "DAMAGE");
            DamageRecipient.Damage(damageValue, owner);
            return true;
        } 
        return false;
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
    public virtual bool CheckVictim(GameObject possibleVictim, ref IHealthAndDamage damageRecipient)
    {
        if (!isAttack)
            return false;
        //Debug.Log(possibleVictim.name);
        if (possibleVictim.transform.root != null) possibleVictim = possibleVictim.transform.root.gameObject;
        //Debug.Log(possibleVictim.name);
        damageRecipient = possibleVictim.GetComponent<IHealthAndDamage>();
        //Debug.Log(possibleVictim.GetComponent<IHealthAndDamage>());
        if (damageRecipient != null) return true;
        else return false;
    }
}
