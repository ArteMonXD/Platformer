using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractive
{
    abstract void Execute<T>(T passedObject = default);
}
