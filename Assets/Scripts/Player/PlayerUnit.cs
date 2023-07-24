using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class PlayerUnit : Unit, ISuperAttack
{
    [Header("JumpVars")]
    [SerializeField] private byte jumpCounter;
    [SerializeField] private byte jumpCount;
    [SerializeField] private bool isWater;
    [Header("Super Attack Vars")]
    [SerializeField] private Transform superAttackStartPoint;
    [SerializeField] private GameObject superAttackPrefab;
    [SerializeField] float currentCharge;
    public float Charge => currentCharge;
    [SerializeField] float maxCharge;
    public float MaxCharge => maxCharge;
    [SerializeField] private float chargeProfit;
    public float ChargeProfit => chargeProfit;
    [SerializeField] float[] superAttackPrice;
    public float[] SuperAttackPrice => superAttackPrice;
    [SerializeField] protected float[] damageSuperAttack;
    public float[] DamageSuperAttack => damageSuperAttack;
    [SerializeField] protected int superAttackCountVariant;
    public int SuperAttackCountVariant => superAttackCountVariant;
    [SerializeField] protected int superAttackCounter;
    public int SuperAttackCounter => superAttackCounter;
    [SerializeField] protected bool isSuperAttacking;
    public bool IsSuperAttacking => isSuperAttacking;
    [Header("Animation")]
    [SerializeField] private EventHundle[] animationEvent_SA;
    public EventHundle[] AnimationEvent_SA => animationEvent_SA;
    public delegate void DiedPlayerHandle();
    public event DiedPlayerHandle DiedPlayerEvent;
    
    void Start()
    {
        InitUnit();
        EventHundle[] eventHundles = animationCollection.GetComponents<EventHundle>();
        animationEvent_SA = new EventHundle[superAttackCountVariant];
        for (int i = 0; i < superAttackCountVariant; i++) animationEvent_SA[i] = eventHundles[eventHundles.Length - superAttackCountVariant + i]; 
        for(int i = 0; i<superAttackCountVariant; i++) animationEvent_SA[i]._event += SuperAttackDo;
        UI_Interface.Instance.ValueUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationStatusControl();
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    public override void Damage(float damage, IAttack attacker)
    {
        base.Damage(damage, attacker);
        UI_Interface.Instance.ValueUpdate();
    }
    public override void Heal(float healValue)
    {
        base.Heal(healValue);
        UI_Interface.Instance.ValueUpdate();
    }
    public override void Death()
    {
        base.Death();
        DiedPlayerEvent?.Invoke();
    }
    public override void AttackInput(bool attackInput)
    {
        if(isDead)
            return;

        isAttacking = attackInput;
        if (isAttacking)
        {
            if (isGround)
                AttackInit();
            else if (isCrouch)
                CrouchAttackInit();
            else if (isJump || isFall)
                FlyAttackInit();
        }
    }
    public void SuperAttackInput(bool superAttackInput)
    {
        if(isDead)
            return;

        if (superAttackInput)
        {
            if (currentCharge >= superAttackPrice[superAttackCounter])
            {
                isSuperAttacking = superAttackInput;
                SuperAttackInit();
            }
        }
        else isSuperAttacking = superAttackInput;
    }

    public void SuperAttackInit()
    {
        if (damageDealer.DamageOwner == null)
            damageDealer.SetAttack(damageSuperAttack[superAttackCounter], this);
        else
            damageDealer.SetAttack(damageSuperAttack[superAttackCounter]);
    }
    public void SuperAttackDo()
    {
        ChargeDecrease(superAttackPrice[superAttackCounter]);
        GameObject currentSA = Instantiate(superAttackPrefab, superAttackStartPoint.position, superAttackStartPoint.rotation);
        PlayerSuperAttack objectSA = currentSA.GetComponent<PlayerSuperAttack>();
        objectSA.SetAttack(damageSuperAttack[superAttackCounter], this);
    }
    public override void AttackInit()
    {
        if (timerAttackReset != null) StopCoroutine(timerAttackReset);
        base.AttackInit();
    }
    public override void FlyAttackInit()
    {
        if (timerFlyAttackReset != null) StopCoroutine(timerFlyAttackReset);
        base.FlyAttackInit();
    }
    public override void CrouchAttackInit()
    {
        if (timerCrouchAttackReset != null) StopCoroutine(timerCrouchAttackReset);
        base.CrouchAttackInit();
    }
    
    public override void AttackDo()
    {
        base.AttackDo();
        timerAttackReset = StartCoroutine(AttackReset());
    }
    public override void FlyAttackDo()
    {
        base.FlyAttackDo();
        timerFlyAttackReset = StartCoroutine(FlyAttackReset());
    }
    public override void CrouchAttackDo()
    {
        base.CrouchAttackDo();
        timerCrouchAttackReset = StartCoroutine(CrouchAttackReset());
    }
    public void UpCharge(float value)
    {
        currentCharge += value;
        if (currentCharge > maxCharge) currentCharge = maxCharge;
        UI_Interface.Instance.ValueUpdate();
    }
    public void ChargeDecrease(float value)
    {
        currentCharge -= value;
        if(currentCharge < 0) currentCharge = 0;
        UI_Interface.Instance.ValueUpdate();
    }
    public override void AnimationStatusControl()
    {
        if (isDead)
        {
            if (!isDeathAnimation)
            {
                animator.SetTrigger(GlobalVar.PA_DEATH);
                isDeathAnimation = true;
            }
        }            
        animator.SetBool(GlobalVar.PA_RUNING, isRuning);
        animator.SetBool(GlobalVar.PA_ATTACK, isAttacking);
        animator.SetInteger(GlobalVar.PA_ATTACK_COUNTER, attackCounter);
        animator.SetBool(GlobalVar.PA_JUMPING, isJump);
        animator.SetBool(GlobalVar.PA_FALLING, isFall);
        animator.SetBool(GlobalVar.PA_GROUNDED, isGround);
        if(isSuperAttacking)
            animator.SetTrigger(GlobalVar.PA_SUPER_ATTACK);
    }
    public override void Move(float horizontalInput)
    {
        if(!isAttacking && !isSuperAttacking)
        {
            base.Move(horizontalInput);
        }  
    }
    public override bool JumpCheck()
    {
        if (isWater)
        {
            jumpCounter = 1;
            return true;
        }
        else if (base.JumpCheck() || (jumpCounter < jumpCount))
            return true;
        else
            return false;
    }
    public override void Jump()
    {
        base.Jump();
        isAttacking = false;
        if(!isWater)
            jumpCounter++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer(GlobalVar.WATER_LAYER)) isWater = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GlobalVar.WATER_LAYER)) isWater = false;
    }
    public override void CheckGround()
    {
        base.CheckGround();
        Vector2 footColliderPos = footColliderTransform.position;
        if (Physics2D.OverlapCircle(footColliderPos, jumpOffset, groundLayers))
            jumpCounter = 0;
    }
}
