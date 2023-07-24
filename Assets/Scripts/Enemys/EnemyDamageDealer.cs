using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : UnitDamageDealer
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealthAndDamage damageRecipient = null;
        if(CheckVictim(collision.gameObject, ref damageRecipient))
        {
            MakeDamage(damageRecipient, damage);
        }
    }
}
