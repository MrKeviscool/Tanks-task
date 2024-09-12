using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class tankHealth : MonoBehaviour
{
    [SerializeField] float startingHealth = 100f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float currentHealth;
    bool dead = false;
    ParticleSystem exploParticles;

    [SerializeField] float stunMs = 1000f;
    private float enemySpeed;
    private float enemyRotation;
    private NavMeshAgent nma;

    private void Awake()
    {
        exploParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();

        exploParticles.gameObject.SetActive(false);
        nma = gameObject.GetComponent<NavMeshAgent>();
        if (nma != null){
            enemySpeed = nma.speed;
            enemyRotation = nma.angularSpeed;
        }
    }

    private void OnEnable()
    {
        currentHealth = startingHealth;
        dead = false;
    }
    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 1)
            onDeath();
        //if (gameObject.tag == "Enemy")
            stun();
    }
    public void stun()
    {
        StopCoroutine(stunDuration(0));
        StartCoroutine(stunDuration(stunMs/1000));
    }
    void onDeath()
    {
        dead = true;
        exploParticles.transform.position = transform.position;
        exploParticles.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }

    IEnumerator stunDuration(float stunMs)
    {
        if(nma != null)
        {
            nma.speed = 0;
            nma.angularSpeed = 0;
            nma.gameObject.GetComponent<enemyShoot>().stunned = true;
        }
        yield return new WaitForSeconds(stunMs);
        //unstun
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        if (nma != null)
        {
            nma.speed = enemySpeed;
            nma.angularSpeed = enemyRotation;
            nma.gameObject.GetComponent<enemyShoot>().stunned = false;
        }
    }
}