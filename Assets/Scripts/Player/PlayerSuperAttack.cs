using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperAttack : UnitDamageDealer
{
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed { get => bulletSpeed; }
    [SerializeField] private Vector2 velocity;
    public Vector2 Velocity => velocity;
    [SerializeField] private float lifeTime;
    private void Start()
    {
    }
    public override void SetAttack(float damageValue, IAttack setOwner)
    {
        base.SetAttack(damageValue, setOwner);
        velocity = new Vector2(1f * bulletSpeed, 0f);
        Destroy(gameObject, lifeTime);
    }
    public override bool MakeDamage(IHealthAndDamage DamageRecipient, float damageValue)
    {
        return base.MakeDamage(DamageRecipient, damageValue);
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right, bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealthAndDamage damageRecipient = null;

        if (CheckVictim(collision.gameObject, ref damageRecipient))
        {
            if ((damageRecipient as Unit) == (owner as Unit))
                return;
            MakeDamage(damageRecipient, damage);
            
            Destroy(gameObject);
        }
        else Destroy(gameObject); 
    }
}
