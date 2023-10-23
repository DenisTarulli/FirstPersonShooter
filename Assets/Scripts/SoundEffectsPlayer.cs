using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip shoot, reload;

    public void ShootSound()
    {
        audioSrc.clip = shoot;
        audioSrc.Play();
    }

    public void ReloadSound()
    {
        audioSrc.clip = reload;
        audioSrc.Play();
    }
}
