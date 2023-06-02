using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameGround : MonoBehaviour, IInteractive
{
    [SerializeField] private Animator animator;
    public void Execute<T>(T passedObject = default)
    {
        animator.Play("FlameGround");
    }
}
