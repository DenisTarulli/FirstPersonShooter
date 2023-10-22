using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;

    [SerializeField] private Camera fpsCam;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    private void Shoot()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
        {
            Debug.Log(hit.transform.name);
        }
       
    }
}
