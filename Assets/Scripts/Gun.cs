using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float impactForce = 30f;
    [SerializeField] private float fireRate = 15f;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private SoundEffectsPlayer sfxPlayer;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject defaultImpactEffect;
    [SerializeField] private GameObject bloodImpactEffect;


    private const string IS_ENEMY = "Enemy";
    private const string IS_FIRE = "Fire";
    private float nextTimeToFire = 0f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ShootInputAndAnimation();
    }

    private void ShootInputAndAnimation()
    {
        if (Time.time >= nextTimeToFire)
        {
            animator.SetInteger(IS_FIRE, -1);
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            animator.SetInteger(IS_FIRE, 2);
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }        
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        sfxPlayer.ShootSound();

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
        {         
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            if (hit.transform.tag == IS_ENEMY)
            {
                GameObject impactGameObject = Instantiate(bloodImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGameObject, 1f);
            }
            else
            {
                GameObject impactGameObject = Instantiate(defaultImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGameObject, 1f);
            }            
        }
    }
}
