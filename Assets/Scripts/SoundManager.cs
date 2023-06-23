using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip[] clip;




    public void Playsound()
    {
        int randomclip = Random.Range(0,clip.Length);
        AudioSource audio = FindObjectOfType<AudioSource>();
        audio.PlayOneShot(clip[randomclip]);

    }
}
