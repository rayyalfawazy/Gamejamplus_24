using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleAudio : MonoBehaviour
{
    [SerializeField] private AudioSource soundEffects;

    [SerializeField] private AudioClip correctSfx;
    [SerializeField] private AudioClip incorectSfx;

    public void PlaySFX(string sfx)
    {
        if (sfx == "Correct")
        {
            soundEffects.clip = correctSfx;
            
        } else if (sfx == "Incorrect")
        {
            soundEffects.clip = incorectSfx;
        }
        soundEffects.Play();
    }
}
