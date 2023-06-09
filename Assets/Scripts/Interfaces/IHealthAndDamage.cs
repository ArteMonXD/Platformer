using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthAndDamage
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public bool IsDead { get; set; }

    public void Heal(float healValue);
    public void Damage(float damage);
    public abstract void CheckDeath();
    public abstract void Death();
}
