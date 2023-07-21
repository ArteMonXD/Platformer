using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthAndDamage
{
    public float Health { get;}
    public float MaxHealth { get;}
    public bool IsDead { get;}
    public bool IsHyperArmor { get; }

    public void Heal(float healValue);
    public void Damage(float damage);
    public abstract void CheckDeath();
    public abstract void Death();
}
