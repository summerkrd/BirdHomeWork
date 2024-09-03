using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundSound;
    [SerializeField] private AudioSource _hitSound;
    [SerializeField] private AudioSource _flapSound;

    public void PlayBackgroundSound(bool play)
    {
        if (play)
            _backgroundSound.Play();
        else
            _backgroundSound.Stop();
    }

    public void PlayHitSound()
    {
        _hitSound.Play();
    }

    public void PlayFlapSound()
    {
        _flapSound.Play();
    }
}
