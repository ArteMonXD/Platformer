using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Unit : MonoBehaviour, IAttack, IHealthAndDamage, IMovement
{
    //0 - Damage Value For Super Attack
    //1 - Damage Value For Attack1
    //2 - Damage Value For Attack2
    //    ...
    [SerializeField] protected byte attackVariantCount;
    public byte AttackVariantCount => attackVariantCount;
    [SerializeField] protected float[] damageAttack;
    public float this[int index] { get { return damageAttack[index];} }
    public float[] DamageAttack { get { return damageAttack; }}

    [SerializeField] protected Animator animator;
    public Animator Animator { get { return animator; } }

    protected int attackCounter;
    public int AttackCounter { get { return attackCounter; } }

    [SerializeField] protected bool isAttacking;
    public bool IsAttacking { get { return isAttacking; } }

    protected float health;
    public float Health { get { return health; } set => health = value; }

    [SerializeField] protected float maxHealth;
    public float MaxHealth { get { return maxHealth; } set => maxHealth = value; }

    [SerializeField] protected bool isDead;
    public bool IsDead { get { return isDead; } set => isDead = value; }
    [SerializeField] protected UnitDamageDealer damageDealer;
    public IDamageDealer Dealer { get { return damageDealer; }}

    protected Rigidbody2D rb;
    public Rigidbody2D m_RigidBody { get => rb; }

    [SerializeField] protected float jumpForce;
    public float JumpForce { get => jumpForce; }

    [SerializeField] protected float speedWalk;
    public float SpeedWalk { get => speedWalk; }
    [SerializeField] protected bool isGround;
    public bool IsGround { get => isGround; }

    public virtual void CheckDeath()
    {
        if (health <= 0)
            Death();
    }

    public virtual void Damage(float damage)
    {
        health -= damage;
        CheckDeath();
    }

    public virtual void Death()
    {
        isDead = true;
    }

    public virtual void Heal(float healValue)
    {
        if ((health + healValue) > maxHealth)
            health = maxHealth;
        else
            health += healValue;
    }

    public virtual void AttackInit()
    {
        isAttacking = true;
        if (damageDealer.DamageOwner == null)
            damageDealer.SetAttack(damageAttack[attackCounter + 1], this);
        else
            damageDealer.SetAttack(damageAttack[attackCounter + 1]);
    }

    public virtual void AttackDo()
    {
        attackCounter++;
        if(attackCounter >= attackVariantCount)
            attackCounter = 0;
    }

    public virtual void Movement(float horizontalInput, bool jumpInput)
    {
        if (jumpInput)
            Jump();
    }

    public virtual void Move()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Jump()
    {
        if (isGround)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public virtual void CheckGround()
    {
        throw new System.NotImplementedException();
    }
}
