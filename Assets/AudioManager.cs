using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---AudioSource---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("---AudioClip---")]
    public AudioClip CowDeath;
    public AudioClip DogDeath;
    public AudioClip PigDeath;
    public AudioClip SheepDeath;
    public AudioClip PickUpBrain;
    public AudioClip Zombie1Death;
    public AudioClip Zombie2Death;
    public AudioClip Zombie1Spawn;
    public AudioClip Zombie2Spawn;


    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
