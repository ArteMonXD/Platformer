using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerUnit : Unit, ISuperAttack
{
    [Header("Super Attack Vars")]
    [SerializeField] private Transform superAttackStartPoint;
    [SerializeField] private GameObject superAttackPrefab;
    [SerializeField] float currentCharge;
    public float Charge => currentCharge;
    [SerializeField] float maxCharge;
    public float MaxCharge => maxCharge;
    [SerializeField] float superAttackPrice;
    public float SuperAttackPrice => superAttackPrice;
    [SerializeField] protected byte superAttackVariantCount;
    public byte SuperAttackVariantCount => superAttackVariantCount;
    [SerializeField] protected float[] damageSuperAttack;
    public float[] DamageSuperAttack => damageSuperAttack;
    [SerializeField] protected int superAttackCounter;
    public int SuperAttackCounter => superAttackCounter;
    [SerializeField] protected bool isSuperAttacking;
    public bool IsSuperAttacking => isSuperAttacking;

    [SerializeField] private GameObject animationCollection;
    [SerializeField] private EventHundle animationEvent_SA;
    [SerializeField] private EventHundle[] animationEvent_A;
    
    void Start()
    {
        EventHundle[] eventHundles = animationCollection.GetComponents<EventHundle>();
        animationEvent_SA = eventHundles[0];
        animationEvent_A = new EventHundle[eventHundles.Length - 1];

        for(int i = 0; i < eventHundles.Length -1; i++) animationEvent_A[i] = eventHundles[i + 1];

        animationEvent_SA._event += SuperAttackDo;
        int counter = 0;
        for (int i = 0; i < attackVariantCount; i++, counter++) animationEvent_A[counter]._event += AttackDo;
        for (int i = 0; i < crouchAttackCounter; i++, counter++) animationEvent_A[counter]._event += CrouchAttackDo;
        for(int i = 0; i < flyAttackCounter; i++, counter++) animationEvent_A[counter]._event += FlyAttackDo;

        rb = GetComponent<Rigidbody2D>();
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

    }
    public void SuperAttackInit()
    {
        if (currentCharge >= superAttackPrice)
        {
            animator.SetTrigger(GlobalVar.PA_SUPER_ATTACK);
            if (damageDealer.DamageOwner == null)
                damageDealer.SetAttack(damageSuperAttack[superAttackCounter], this);
            else
                damageDealer.SetAttack(damageSuperAttack[superAttackCounter]);
        }
    }
    public void SuperAttackDo()
    {
        currentCharge -= superAttackPrice;
        GameObject currentSA = Instantiate(superAttackPrefab, superAttackStartPoint.position, superAttackStartPoint.rotation);
        PlayerSuperAttack objectSA = currentSA.GetComponent<PlayerSuperAttack>();
        objectSA.SetAttack(damageSuperAttack[0], this);
        Rigidbody2D currentSA_RB = currentSA.GetComponent<Rigidbody2D>();
        currentSA_RB.velocity = new Vector2(1 * objectSA.BulletSpeed, currentSA_RB.velocity.y);
        superAttackCounter++;
        if (superAttackCounter >= superAttackVariantCount)
            superAttackCounter = 0;
    }
    public void UpCharge(float value)
    {
        currentCharge += value;
        if (currentCharge > maxCharge) currentCharge = maxCharge;
    }
    public override void AnimationStatusControl()
    {
        animator.SetBool(GlobalVar.PA_RUNING, isRuning);
        animator.SetBool(GlobalVar.PA_ATTACK, isAttacking);
        animator.SetInteger(GlobalVar.PA_ATTACK_COUNTER, attackCounter);
        animator.SetBool(GlobalVar.PA_JUMPING, isJump);
        animator.SetBool(GlobalVar.PA_FALLING, isFall);
        animator.SetBool(GlobalVar.PA_GROUNDED, isGround);
    }
    public override void Move(float horizontalInput)
    {
        if(!isAttacking && !isSuperAttacking)
        {
            base.Move(horizontalInput);
        }  
    }
    public override void Jump()
    {
        base.Jump();
        if(isGround)
            isAttacking = false;
    }
#if UNITY_EDITOR
    [ContextMenu ("Super Attack")]
    public void DebugSuperAttackActivate() 
    {
        SuperAttackInit();
    }
#endif
}
