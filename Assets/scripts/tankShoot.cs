using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class tankShoot : MonoBehaviour
{
    [SerializeField] Rigidbody shell;
    [SerializeField] Transform shootPoint;
    [SerializeField] float force = 30f;
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
            fire();
    }

    void fire()
    {
        Rigidbody shellInstance = Instantiate(shell, shootPoint.position, shootPoint.rotation);
        shellInstance.velocity = force * shootPoint.forward;
    }
}
