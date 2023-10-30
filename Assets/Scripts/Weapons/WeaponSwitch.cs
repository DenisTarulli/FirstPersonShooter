using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private Gun gun;
    [SerializeField] private GameObject gunIcon;
    [SerializeField] private GameObject grenadeIcon;
    [SerializeField] private PauseMenu pauseMenu;
    [HideInInspector] public int selectedWeapon = 0;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        if (gun.isReloading) return;
        
        int previousSelectedWeapon = selectedWeapon;

        if (!pauseMenu.gameIsPaused)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                selectedWeapon = 0;

            if (Input.GetKeyDown(KeyCode.Alpha2))
                selectedWeapon = 1;

            if (previousSelectedWeapon != selectedWeapon)
                SelectWeapon();

            if (selectedWeapon == 0)
            {
                gunIcon.GetComponent<Image>().color = Color.white;
                grenadeIcon.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                gunIcon.GetComponent<Image>().color = Color.grey;
                grenadeIcon.GetComponent<Image>().color = Color.white;
            }
        }        
    }

    

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
