using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : UnitDamageDealer
{
    public override bool MakeDamage(IHealthAndDamage DamageRecipient, float damageValue)
    {
        return base.MakeDamage(DamageRecipient, damageValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealthAndDamage damageRecipient = null;
        if (CheckVictim(collision.gameObject, ref damageRecipient))
        {
            if (MakeDamage(damageRecipient, damage))
            {
                PlayerUnit player = (owner as PlayerUnit);
                player.UpCharge(player.ChargeProfit);
            }
        }
    }
}
