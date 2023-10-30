using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject objectToThrow;
    [SerializeField] private WeaponSwitch weaponSwitch;
    [SerializeField] private TextMeshProUGUI grenadeText;
    [SerializeField] private PauseMenu pauseMenu;

    [Header("Settings")]
    [SerializeField] private int totalThrows;
    [SerializeField] private float throwCooldown;

    [Header("Throwing")]
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;

    private KeyCode throwKey = KeyCode.Mouse0;
    private bool readyToThrow;

    private void Start()
    {
        grenadeText.text = $"{totalThrows}";
        readyToThrow = true;
    }

    private void Update()
    {
        if (Input.GetKey(throwKey) && readyToThrow && totalThrows > 0 && weaponSwitch.selectedWeapon == 1 && !pauseMenu.gameIsPaused)
        {            
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
            forceDirection = (hit.point - attackPoint.position).normalized;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        grenadeText.text = $"{totalThrows}";

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
