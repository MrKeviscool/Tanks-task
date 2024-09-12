using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] float maxLifeTime = 2f;
    [SerializeField] float maxDamage  = 50f;
    [SerializeField] float exploRad = 5f;
    [SerializeField] float exploForce = 100f;
    [SerializeField] ParticleSystem exploParticles;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
        if(targetRb != null) {
            targetRb.AddExplosionForce(exploForce, transform.position, exploRad);
            tankHealth tHealth = targetRb.gameObject.GetComponent<tankHealth>();
            if(tHealth != null)
            {
                float damage = calcDamge(targetRb.position);
                tHealth.takeDamage(damage);
            }
        }
        exploParticles = Instantiate(exploParticles, transform.position, transform.rotation);
        //exploParticles.transform.parent = null;
        exploParticles.Play();
        Destroy(exploParticles.gameObject, exploParticles.main.duration);
        Destroy(gameObject);
    }

    float calcDamge(Vector3 targetPos)
    {
        Vector3 exploToTarg = targetPos - transform.position;
        float explosionDistance = exploToTarg.magnitude;
        float relativeDistance = (exploRad - explosionDistance) / exploRad;
        float damage = relativeDistance * maxDamage;
        damage = Mathf.Max(0f, damage);
        return damage;
    }
}
