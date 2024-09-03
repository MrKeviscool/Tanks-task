using UnityEngine;
using UnityEngine.AI;

public class enemyTankMove : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float closeDistance = 8f;
    [SerializeField] Transform turret;
    GameObject player;
    NavMeshAgent agent;
    Rigidbody rb;

    bool follow = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        follow = false;
    }
    private void OnEnable()
    {
        rb.isKinematic = false;
    }
    private void OnDisable()
    {
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            follow = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            follow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!follow)
            return;
        float distance = (player.transform.position - transform.position).magnitude;
        if (distance > closeDistance)
        {
            agent.SetDestination(player.transform.position);
            agent.isStopped = false;
        }
        else
            agent.isStopped = true;
        if (turret)
            turret.LookAt(player.transform.position);
    }
}
