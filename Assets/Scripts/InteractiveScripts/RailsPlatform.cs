using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailsPlatform : MonoBehaviour, IInteractive
{
    private SliderJoint2D sliderJoint;
    private Vector3 lastPosition = Vector3.zero;
    private Coroutine currentCoroutine = null;
    private Coroutine checkCoroutine = null;
    [SerializeField] private bool switchMotor;
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
        CheckPlatformPosition();
    }

    private IEnumerator SwitchMotor(SliderJoint2D passedObject)
    {
        yield return new WaitForSeconds(1f);
        JointMotor2D tempMotor = passedObject.motor;
        tempMotor.motorSpeed *= -1;
        passedObject.motor = tempMotor;
        currentCoroutine = null;
        lastPosition = Vector3.zero;
        switchMotor = false;
    }
    private void CheckPlatformPosition()
    {
        if (checkCoroutine == null)
            checkCoroutine = StartCoroutine(DelayCheck());
    }
    private IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(0.1f);
        if (transform.position != lastPosition)
            lastPosition = transform.position;
        else
            SwitchMotor();
        checkCoroutine = null;
    }

    private void SwitchMotor()
    {
        if (!switchMotor)
        {
            Execute(sliderJoint);
            switchMotor = true;
        }
    }
}
