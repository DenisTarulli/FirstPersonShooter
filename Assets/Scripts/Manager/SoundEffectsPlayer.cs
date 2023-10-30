using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioSource audioSrcMusic;
    [SerializeField] private AudioClip shoot, reload, emptyChamber, win, lose;


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

    public void emptyChamberSound()
    {
        audioSrc.clip = emptyChamber;
        audioSrc.Play();
    }

    public void WinSound()
    {
        audioSrc.clip = win;
        audioSrc.Play();
    }

    public void LoseSound()
    {
        audioSrc.clip = lose;
        audioSrc.Play();
    }
}
