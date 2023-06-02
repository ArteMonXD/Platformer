using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLamp : MonoBehaviour, IInteractive
{
    public void Execute<T>(T passedObject = default)
    {
        (passedObject as GameObject).GetComponent<IInteractive>().Execute<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("FlameGround"))
            Execute(collision.gameObject);
    }
}
