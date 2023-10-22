using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip shoot;

    public void ShootSound()
    {
        audioSrc.clip = shoot;
        audioSrc.Play();
    }
}
