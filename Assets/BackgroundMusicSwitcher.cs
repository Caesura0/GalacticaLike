using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicSwitcher : MonoBehaviour
{

    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip sceneOneMusic;
    [SerializeField] AudioClip sceneTwoMusic;
    [SerializeField] AudioClip gameOverMusic;

    AudioSource audioSource;
   
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("audioPlayer").GetComponent<AudioSource>();

    }


}
