using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip musicOnStart;

    AudioSource audioSource;
    AudioClip switchTo;

    [SerializeField] private float volumeSetting = .2f;
    private float volume;
    [SerializeField] private float timeToSwitch;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Play(musicOnStart, true);
    }

    public void Play(AudioClip music, bool interrupt=false)
    {
        if (interrupt)
        {
            volume = volumeSetting;
            audioSource.volume = volume;
            audioSource.clip = music;
            audioSource.Play();
        }
        else
        {
            switchTo = music;
            StartCoroutine(SmoothSwitchMusic());
        }
    }

    
    IEnumerator SmoothSwitchMusic()
    {
        volume = volumeSetting;

        while (volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;
            if (volume < 0f)
            {
                volume = 0f;
            }

            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);
    }
}
