using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float impactForce = 30f;
    [SerializeField] private float fireRate = 15f;

    public int maxAmmo = 30;
    [HideInInspector] public int currentAmmo;
    [SerializeField] private float reloadTime = 1f;
    [HideInInspector] public bool isReloading;
    private bool outOfAmmo;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private SoundEffectsPlayer sfxPlayer;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject defaultImpactEffect;
    [SerializeField] private GameObject bloodImpactEffect;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private PauseMenu pauseMenu;

    private const string IS_ENEMY = "Enemy";
    private const string IS_FIRE = "Fire";
    private const string IS_RELOADING = "IsReloading";
    private float nextTimeToFire = 0f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentAmmo = maxAmmo;
        outOfAmmo = false;
    }    

    private void Update()
    {
        if (isReloading)
            return;

        ShootInputAndAnimation();
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool(IS_RELOADING, true);
        animator.SetInteger(IS_FIRE, -1);

        sfxPlayer.ReloadSound();

        yield return new WaitForSeconds(reloadTime);


        currentAmmo = maxAmmo;
        ammoText.text = $"{currentAmmo:D2}/{maxAmmo}";
        outOfAmmo = false;
        animator.SetBool(IS_RELOADING, false);
        isReloading = false;
    }

    private void ShootInputAndAnimation()
    {
        if (Time.time >= nextTimeToFire)
        {
            animator.SetInteger(IS_FIRE, -1);
        }

        if (Input.GetButtonDown("Fire1") && outOfAmmo && !pauseMenu.gameIsPaused)
            sfxPlayer.emptyChamberSound();

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && !outOfAmmo && !pauseMenu.gameIsPaused)
        {   
            animator.SetInteger(IS_FIRE, 2);
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();                       
        }        
        
        if (Input.GetKey(KeyCode.R) && currentAmmo != maxAmmo && !pauseMenu.gameIsPaused)
            StartCoroutine(Reload());
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

        currentAmmo--;
        ammoText.text = $"{currentAmmo:D2}/{maxAmmo}";
        if (currentAmmo == 0)
            outOfAmmo = true;
    }
}
