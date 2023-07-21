using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Unit : MonoBehaviour, IAttack, IHealthAndDamage, IMovement, IAnimation
{
    //0 - Damage Value For Super Attack
    //1 - Damage Value For Attack1
    //2 - Damage Value For Attack2
    //    ...
    [Header("Attack Vars")]
    [SerializeField] protected byte attackVariantCount;
    public byte AttackVariantCount => attackVariantCount;
    [SerializeField] protected byte crouchAttackVariantCount;
    public byte CrouchAttackVariantCount => crouchAttackVariantCount;
    [SerializeField] protected byte flyAttackVariantCount;
    public byte FlyAttackVariantCount => flyAttackVariantCount;
    [SerializeField] protected float[] damageAttack;
    public float[] DamageAttack => damageAttack;
    [SerializeField] protected float[] damageCrouchAttack;
    public float[] DamageCrouchAttack => damageCrouchAttack;
    [SerializeField] protected float[] damageFlyAttack;
    public float[] DamageFlyAttack => damageFlyAttack;
    [SerializeField] protected int attackCounter;
    public int AttackCounter => attackCounter;
    [SerializeField] protected int crouchAttackCounter;
    public int CrouchAttackCounter => crouchAttackCounter;
    [SerializeField] protected int flyAttackCounter;
    public int FlyAttackCounter => flyAttackCounter;
    [SerializeField] protected bool isAttacking;
    public bool IsAttacking => isAttacking;

    [SerializeField] protected UnitDamageDealer damageDealer;
    public IDamageDealer Dealer => damageDealer;

    protected float health;
    public float Health => health;

    [Header("Health Vars")]
    [SerializeField] protected float maxHealth;
    public float MaxHealth => maxHealth;

    [SerializeField] protected bool isDead;
    public bool IsDead => isDead;
    [SerializeField] protected bool isHyperArmor;
    public bool IsHyperArmor => isHyperArmor;
    protected Rigidbody2D rb;
    public Rigidbody2D m_RigidBody => rb;

    [Header("Move Vars")]
    [SerializeField] protected float jumpForce;
    public float JumpForce => jumpForce;

    [SerializeField] protected AnimationCurve speedWalk;
    public AnimationCurve SpeedWalk => speedWalk;
    [SerializeField] protected bool isRuning;
    public bool IsRuning => isRuning;
    [SerializeField] protected bool isJump;
    public bool IsJump => isJump;
    [SerializeField] protected bool isFall;
    public bool IsFall => isFall;
    [SerializeField] protected bool isCrouch;
    public bool IsCrouch => isCrouch;
    [SerializeField] protected bool isGround;
    public bool IsGround => isGround;
    [SerializeField] protected float jumpOffset;
    public float JumpOffset  => jumpOffset; 
    [SerializeField] protected Transform footColliderTransform;
    public Transform FootCollider  => footColliderTransform; 
    [SerializeField] protected LayerMask groundLayers;
    public LayerMask GroundLayers => groundLayers;

    [Header("Animation Vars")]
    [SerializeField] protected Animator animator;
    public Animator Animator  => animator; 
    private Coroutine switchJumpCoroutine;
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
    public virtual void AttackInput(bool attackInput)
    {
        isAttacking = attackInput;
        if(isAttacking)
            AttackInit();
    }
    public virtual void AttackInit()
    {
        if (damageDealer.DamageOwner == null)
            damageDealer.SetAttack(damageAttack[attackCounter], this);
        else
            damageDealer.SetAttack(damageAttack[attackCounter]);
    }
    public virtual void AttackDo()
    {
        attackCounter++;
        if(attackCounter >= attackVariantCount)
            attackCounter = 0;
    }

    public virtual void CrouchAttackInit()
    {
        if (damageDealer.DamageOwner == null)
            damageDealer.SetAttack(damageCrouchAttack[crouchAttackCounter], this);
        else
            damageDealer.SetAttack(damageCrouchAttack[crouchAttackCounter]);
    }
    public virtual void CrouchAttackDo()
    {
        crouchAttackCounter++;
        if(crouchAttackCounter >= crouchAttackVariantCount)
            crouchAttackCounter = 0;
    }
    public virtual void FlyAttackInit()
    {
        if (damageDealer.DamageOwner == null)
            damageDealer.SetAttack(damageFlyAttack[flyAttackCounter], this);
        else
            damageDealer.SetAttack(damageFlyAttack[flyAttackCounter]);
    }
    public virtual void FlyAttackDo()
    {
        flyAttackCounter++;
        if(flyAttackCounter >= flyAttackVariantCount)
            flyAttackCounter = 0;
    }
    public virtual void Movement(float horizontalInput, bool jumpInput)
    {
        if (jumpInput)
        {
            Jump();
            isJump = true;
            if (switchJumpCoroutine != null)
                StopCoroutine(switchJumpCoroutine);
            switchJumpCoroutine = StartCoroutine(SwitchJumpStatus());
        }

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            Move(horizontalInput);
            isRuning = true;
        }
        else
        {
            isRuning = false;
        }
    }

    public virtual void Move(float horizontalInput)
    {
        rb.velocity = new Vector2(speedWalk.Evaluate(horizontalInput), rb.velocity.y);
    }

    public virtual void Jump()
    {
        if (isGround)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public virtual void CheckGround()
    {
        Vector2 footColliderPos = footColliderTransform.position;
        if(Physics2D.OverlapCircle(footColliderPos, jumpOffset, groundLayers))
        {
            isGround = true;
            isFall = false;
        }
        else
        {
            isGround = false;
            isFall = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalVar.MOVE_PLATFORM_LAYER))
        {
            Debug.Log("Enter" + collision.gameObject.name);
            this.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("Exit" + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalVar.MOVE_PLATFORM_LAYER))
        {
            transform.parent = null;
        }
    }

    public virtual void AnimationStatusControl()
    {
        throw new System.NotImplementedException();
    }

    protected IEnumerator SwitchJumpStatus()
    {
        yield return new WaitForSeconds(0.4f);
        isJump = false;
    }
}
