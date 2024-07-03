using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMan : MonoBehaviour
{
    [Header("-Audio Source-")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-Audio Clip-")]
    public AudioClip bgm;
    public AudioClip pcdeath;
    public AudioClip pchit;
    public AudioClip enemydeath;
    public AudioClip enemyhit;
    public AudioClip slash;

    private void Start()
    {
        musicSource.clip = bgm;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
