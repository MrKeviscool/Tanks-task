using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankAim : MonoBehaviour
{
    [SerializeField] private Transform turret;
    [SerializeField] float minLookRange = 2f;
    private LayerMask mask;
    // Start is called before the first frame update
    private void Awake()
    {
        mask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit pHit;
        if (Physics.Raycast(ray, out pHit, Mathf.Infinity, mask))
        {
            //my shi
            if ((pHit.point - transform.position).magnitude > minLookRange)
                turret.LookAt(pHit.point);
        }
    }
}
