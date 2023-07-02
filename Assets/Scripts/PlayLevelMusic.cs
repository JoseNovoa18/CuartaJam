using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLevelMusic : MonoBehaviour
{
    AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlayChooseCharactersMusic(audioManager.ChooseCharactersMusic);
    }

  
    
    void Update()
    {
        
    }
}
