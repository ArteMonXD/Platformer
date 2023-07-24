using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private float timeDestroy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<IHealthAndDamage>() != null)
        {
            IHealthAndDamage healthAndDamage = collision.transform.root.GetComponent<IHealthAndDamage>();
            healthAndDamage.Death();
            Destroy((healthAndDamage as Unit).gameObject, timeDestroy);
        }
    }
}
