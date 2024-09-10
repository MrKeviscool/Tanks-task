using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    [SerializeField] Rigidbody shell;
    [SerializeField] Transform shootPoint;
    [SerializeField] float force = 30f;
    [SerializeField] float delay = 1f;
    bool canShoot = false;
    float shootTimer = 0;

    public bool stunned = false;
    private void Awake()
    {
        shootTimer = 0;
        canShoot = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && !stunned)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = delay;
                fire();
            }
        }
    }
    void fire()
    {
        Rigidbody shellInst = Instantiate(shell, shootPoint.position, shootPoint.rotation);
        shellInst.velocity = force * shootPoint.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            canShoot = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            canShoot = false;
    }
}
