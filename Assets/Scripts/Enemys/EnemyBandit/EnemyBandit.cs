using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBandit : Enemy
{
    
    private void Start()
    {
        InitUnit();
    }
    private void Update()
    {
        CheckAttack();
        AnimationStatusControl();
    }
    public override void AnimationStatusControl()
    {
        if (isDead)
        {
            if (!isDeathAnimation)
            {
                animator.SetTrigger(GlobalVar.BEA_DEATH);
                isDeathAnimation = true;
            }
        }
        animator.SetBool(GlobalVar.BEA_ATTACK, isAttacking);
        if (isGetDamage)
        {
            animator.SetTrigger(GlobalVar.BEA_HURT);
            isGetDamage = false;
        }
    }
}
