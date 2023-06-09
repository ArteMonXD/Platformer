using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit, ISuperAttack
{
    [Header("Super Attack")]
    [SerializeField] private Transform superAttackStartPoint;
    [SerializeField] private GameObject superAttackPrefab;
    [SerializeField] float currentCharge;
    public float Charge => currentCharge;
    [SerializeField] float maxCharge;
    public float MaxCharge => maxCharge;
    [SerializeField] float superAttackPrice;
    public float SuperAttackPrice => superAttackPrice;
    [SerializeField] private GameObject animationCollection;
    [SerializeField] private EventHundle animationEvent_SA;
    [SerializeField] private EventHundle[] animationEvent_A;
    void Start()
    {
        EventHundle[] eventHundles = animationCollection.GetComponents<EventHundle>();
        Debug.Log(eventHundles.Length);
        animationEvent_SA = eventHundles[0];
        animationEvent_A = new EventHundle[eventHundles.Length - 1];
        for(int i = 0; i < eventHundles.Length -1; i++)
        {
            Debug.Log(i);
            animationEvent_A[i] = eventHundles[i + 1];
        }
            
        animationEvent_SA._event += SuperAttackDo;
        foreach (EventHundle eh in animationEvent_A)
        {
            eh._event += AttackDo;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SuperAttackInit()
    {
        if (currentCharge >= superAttackPrice)
            animator.SetTrigger(GlobalVar.PA_SUPER_ATTACK);
    }
    public void SuperAttackDo()
    {
        GameObject currentSA = Instantiate(superAttackPrefab, superAttackStartPoint.position, superAttackStartPoint.rotation);
        Debug.Break();
        PlayerSuperAttack objectSA = currentSA.GetComponent<PlayerSuperAttack>();
        objectSA.SetAttack(damageAttack[0], this);
        Rigidbody2D currentSA_RB = currentSA.GetComponent<Rigidbody2D>();
        currentSA_RB.velocity = new Vector2(1 * objectSA.BulletSpeed, currentSA_RB.velocity.y);
    }
    public void UpCharge(float value)
    {
        currentCharge += value;
        if (currentCharge > maxCharge)
            currentCharge = maxCharge;
    }

    [ContextMenu ("Super Attack")]
    public void DebugSuperAttackActivate() 
    {
        SuperAttackInit();
    }
}
