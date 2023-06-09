using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : UnitDamageDealer
{
    public override void MakeDamage(IHealthAndDamage DamageRecipient, float damageValue)
    {
        base.MakeDamage(DamageRecipient, damageValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttack)
            return;

        if(collision.tag == GlobalVar.DAMAGED_TAG && collision.GetComponent<IHealthAndDamage>() != null)
        {
            (collision.GetComponent<IHealthAndDamage>() as Unit).Damage(damage);
        }
    }
}
