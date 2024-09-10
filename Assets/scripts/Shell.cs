using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] float maxLifeTime = 2f;
    [SerializeField] float maxDamage  = 2f;
    [SerializeField] float explosRad = 5f;
    [SerializeField] float exploForce = 100f;
    [SerializeField] ParticleSystem exploParticles;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        exploParticles = Instantiate(exploParticles, transform.position, transform.rotation);
        //exploParticles.transform.parent = null;
        exploParticles.Play();
        Destroy(exploParticles.gameObject, exploParticles.main.duration);
        Destroy(gameObject);
    }
}
