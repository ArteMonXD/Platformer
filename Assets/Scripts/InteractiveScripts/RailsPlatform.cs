using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailsPlatform : MonoBehaviour, IInteractive
{
    private SliderJoint2D sliderJoint;
    private Vector3 lastPosition = Vector3.zero;
    private Coroutine currentCoroutine = null;
    public void Execute<T>(T passedObject = default)
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(SwitchMotor(passedObject as SliderJoint2D));
    }

    void Start()
    {
        sliderJoint = GetComponent<SliderJoint2D>();
    }

    void FixedUpdate()
    {
        if(transform.position != lastPosition)
            lastPosition = transform.position;
        else
            Execute(sliderJoint);
    }

    private IEnumerator SwitchMotor(SliderJoint2D passedObject)
    {
        yield return new WaitForSeconds(1f);
        JointMotor2D tempMotor = passedObject.motor;
        tempMotor.motorSpeed *= -1;
        passedObject.motor = tempMotor;
        currentCoroutine = null;
        lastPosition = Vector3.zero;
    }
}
