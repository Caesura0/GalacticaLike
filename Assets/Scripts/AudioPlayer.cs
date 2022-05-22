using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float clipMasterVolume = 1f;

    static AudioPlayer instance;

    public AudioPlayer GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {


        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
                instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void PlayOneShotClip(AudioClip audioclip, float volume)
    {
        Vector3 cameraPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(audioclip, cameraPos, volume * clipMasterVolume);
    }


}
