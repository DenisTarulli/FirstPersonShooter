using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
    [SerializeField] private float delay = 3f;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private float explosionDamage = 100f;

    [SerializeField] private GameObject explosionEffect;
    private AudioSource audioSrc;

    private float countdown;
    bool hasExploded = false;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        GameObject explosionParticles = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(explosionParticles, 2f);

        audioSrc.Play();

        gameObject.transform.localScale = Vector3.zero;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObjects in colliders)
        {
            Rigidbody rb = nearbyObjects.GetComponent<Rigidbody>();
            if (rb != null)            
                rb.AddExplosionForce(explosionForce, transform.position, radius);            

            Enemy enemy = nearbyObjects.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(explosionDamage);

            PlayerActions playerActions = nearbyObjects.GetComponent<PlayerActions>();
            if (playerActions != null)
                playerActions.PlayerTakeDamage(explosionDamage / 4);
        }

        Destroy(gameObject, 2f);        
    }
    
}
