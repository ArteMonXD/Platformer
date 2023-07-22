using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperAttack : UnitDamageDealer
{
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed { get => bulletSpeed; }
    [SerializeField] private Vector2 velocity;
    public Vector2 Velocity => velocity;
    private void Start()
    {
    }
    public override void SetAttack(float damageValue, IAttack setOwner)
    {
        base.SetAttack(damageValue, setOwner);
        velocity = new Vector2(1f * bulletSpeed, 0f);
    }
    public override void MakeDamage(IHealthAndDamage DamageRecipient, float damageValue)
    {
        base.MakeDamage(DamageRecipient, damageValue);
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right, bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colObject = null;
        if (collision.transform.root.gameObject != null)
            colObject = collision.transform.root.gameObject;
        else
            colObject = collision.gameObject;

        if (!isAttack || (colObject.GetComponent<IHealthAndDamage>() != null && (colObject.GetComponent<IHealthAndDamage>() as Unit) == (owner as Unit)))
            return;

        if (colObject.tag == GlobalVar.DAMAGED_TAG && colObject.GetComponent<IHealthAndDamage>() != null)
        {
            MakeDamage(colObject.GetComponent<IHealthAndDamage>(), damage);
        }
        //Debug.Log(collision.gameObject.name);
        //Debug.Log(colObject.name);
        Destroy(gameObject);
    }
}
