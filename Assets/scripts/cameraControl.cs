using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public const float dampTime = 0.2f;
    public Transform target;

    private Vector3 moveVel;
    private Vector3 posToGo;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        posToGo = target.position;
        transform.position = Vector3.SmoothDamp(transform.position, posToGo, ref moveVel, dampTime);
    }
}
