using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{

    [Header("Player Radar")]
    [SerializeField] protected Transform centerPoint;
    [SerializeField] protected float radarRadius;
    [SerializeField] protected LayerMask radarTargetLayer;
    [Header("Enemy Attack Vars")]
    [SerializeField] protected float currentTime;
    [SerializeField] protected float attackTimeInterval;
    protected void CheckAttack()
    {
        if (Radar()) AttackInput(true);
        else
            AttackInput(false);
    }
    protected bool Radar()
    {
        Vector2 centerRadarPos = centerPoint.position;
        return Physics2D.OverlapCircle(centerRadarPos, radarRadius, radarTargetLayer);
    }
    public override void AttackInput(bool attackInput)
    {
        if (isDead)
            return;
        if (attackInput)
        {
            if (currentTime == 0f)
            {
                isAttacking = true;
                AttackInit();
            }
            if (currentTime < attackTimeInterval) currentTime += Time.deltaTime;
            else currentTime = 0f;
        }
        else currentTime = 0f;
    }

    public override void AttackDo()
    {
        isAttacking = false;
        base.AttackDo();
    }

    public override void Death()
    {
        base.Death();
        rb.isKinematic = true;
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
            collider.isTrigger = true;
    }
}
