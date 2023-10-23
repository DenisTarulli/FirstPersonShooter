using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Gun gun;
    [SerializeField] private TextMeshProUGUI ammoText;

    private void Update()
    {
        ammoText.text = $"{gun.currentAmmo:D2}/{gun.maxAmmo}";
    }
}
