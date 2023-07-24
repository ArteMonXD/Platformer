using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireEffect : MonoBehaviour, IDamageDealer
{
    public IAttack DamageOwner => throw new System.NotImplementedException();

    [SerializeField] private float damage;
    public float Damage => damage;

    public bool IsAttack { get; set; }
    [SerializeField] private float timeRange;
    [SerializeField] private List<IHealthAndDamage> healthAndDamages = new List<IHealthAndDamage>();
    [SerializeField] private int healthCount;
    [SerializeField] private float currentTime;
    private void Update()
    {
        CheckVictim();
        if (IsAttack)
            TimerDamage();
    }
    private void CheckVictim()
    {
        IsAttack = healthCount > 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealthAndDamage healthAndDamage = null;
        if (CheckVictim(collision.gameObject, ref healthAndDamage))
        {
            healthAndDamages.Add(healthAndDamage);
            healthCount = healthAndDamages.Count;
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IHealthAndDamage healthAndDamage = null;
        if(CheckVictim(collision.gameObject, ref healthAndDamage))
        {
            int count = 0;
            for (int i = 0; i < healthAndDamages.Count; i++, count++)
            {
                if (healthAndDamage == healthAndDamages[i])
                    break;
            }
            healthAndDamages.RemoveAt(count);
            healthCount = healthAndDamages.Count;
        }
    }
    private void TimerDamage()
    {
        if(currentTime == 0) MakeDamageAll();
        if (currentTime < timeRange) currentTime += Time.deltaTime;
        else currentTime = 0;
    }
    private void MakeDamageAll()
    {
        foreach (IHealthAndDamage healthed in healthAndDamages)
        {
            MakeDamage(healthed, damage);
        }
    }
    public bool MakeDamage(IHealthAndDamage DamageRecipient, float damageValue)
    {
        DamageRecipient.Damage(damageValue, null);
        return true;
    }

    public void SetAttack(float damageValue, IAttack owner)
    {
        throw new System.NotImplementedException();
    }

    public void SetAttack(float damageValue)
    {
        throw new System.NotImplementedException();
    }

    public bool CheckVictim(GameObject possibleVictim, ref IHealthAndDamage damageRecipient)
    {
        if (possibleVictim.layer != LayerMask.NameToLayer(GlobalVar.DAMAGE_LAYER))
        {
            if (possibleVictim.transform.root.GetComponent<IHealthAndDamage>() != null)
            {
                damageRecipient = possibleVictim.transform.root.GetComponent<IHealthAndDamage>();
                return true;
            }
        }
        return false;
    }
}
