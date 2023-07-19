using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
      [SerializeField]
    private AudioSource _audioSource;
    public static AudioManager Instance;
    private AudioMixer _mixer;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _mixer = GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
       
        
        _mixer.SetFloat("music", PlayerPrefs.GetFloat("music"));
        _mixer.SetFloat("Efx", PlayerPrefs.GetFloat("Efx"));
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetVolumeMixer(string name,float volume)
    {
        if (_mixer != null)
        {
            _mixer.SetFloat(name, volume);
        }
        PlayerPrefs.SetFloat(name,volume);
    }

    public void PauseAudio()
    {
        _audioSource.Pause();
    }

    public void ResumeAudio()
    {
        _audioSource.UnPause();
    }
}