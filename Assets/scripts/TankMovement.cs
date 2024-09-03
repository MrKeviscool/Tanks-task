using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed = 20f;
    public float rotationSpeed = 20f;
    private float forwadInput = 0f;
    private float turnInput = 0f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forwadInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        move();
        turn();
    }

    private void move()
    {
        Vector3 targetVelo = transform.forward * forwadInput * speed;
        rb.AddForce(targetVelo - rb.velocity, ForceMode.VelocityChange);
    }

    private void turn()
    {
        float turnVal = turnInput * rotationSpeed * Time.deltaTime;
        Quaternion targetRotation = Quaternion.Euler(0f, turnVal, 0f);
        rb.MoveRotation(transform.rotation * targetRotation);
    }
}
