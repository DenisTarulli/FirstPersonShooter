using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public AudioSource audioSrc;
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
        Debug.Log("A");
        audioSrc.clip = lose;
        audioSrc.Play();
    }
}
