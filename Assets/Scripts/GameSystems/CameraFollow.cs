using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed = 3f;
    [SerializeField] private float yOffset = 2f;
    [SerializeField] private float[] deadZone = { -10f, -5f, 10f, 5f };
    [SerializeField] private Transform target;
    [SerializeField] private bool follow;
    void Start()
    {
        SetFollow();
    }

    void Update()
    {
        if (CheckDeadZone())
            SetFollow();

        Follow();
    }

    private bool CheckDeadZone()
    {
        if ((target.position.x < transform.position.x + deadZone[0]) ||
           (target.position.y < transform.position.y + deadZone[1]) ||
           (target.position.x > transform.position.x + deadZone[2]) ||
           (target.position.y > transform.position.y + deadZone[3]))
            return true;
        else
            return false;
    }
    private void SetFollow()
    {
        follow = true;
    } 
    private void Follow()
    {
        if (follow)
        {
            Vector3 pos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, pos, Time.deltaTime * followSpeed);
            if (transform.position.x <= (pos.x + 0.01f) &&
                transform.position.x >= (pos.x - 0.01f) &&
                transform.position.y <= (pos.y + 0.01f) &&
                transform.position.y >= (pos.y - 0.01f))
                follow = false;
        }
    }
}
