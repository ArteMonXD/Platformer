using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelActivate : MonoBehaviour, IInteractive
{
    [SerializeField] private Animation animation;
    private void Start()
    {
        //animation = GetComponent<Animation>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
            Execute(animation);
    }

    public void Execute<T>(T passedObject = default)
    {
        (passedObject as Animation).Play();
    }
}
