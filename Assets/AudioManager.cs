using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---AudioSource---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("---AudioClip---")]
    public AudioClip MainMenuMusic;
    public AudioClip BattleMusic;
    public AudioClip ChooseCharactersMusic;
    public AudioClip nightAmbience;
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

    public void PlayMainMenuSound()
    {
        sfxSource.PlayOneShot(nightAmbience);
        StartCoroutine(PlayMainMenuMusic());
    }

    public void PlayChooseCharactersMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip, 0.5f);
    }
    public void PlayLevelMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    IEnumerator PlayMainMenuMusic()
    {
        yield return new WaitForSeconds(4);
        musicSource.PlayOneShot(MainMenuMusic);
    }
   

}

