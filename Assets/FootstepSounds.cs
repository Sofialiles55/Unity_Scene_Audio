using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    AudioSource animationSoundPlayer;

    void Start()
    {
        animationSoundPlayer = GetComponent<AudioSource>();
    }

    private void PlayerFootstepSound()
        {

        animationSoundPlayer.Play();
            
        }
        
}
