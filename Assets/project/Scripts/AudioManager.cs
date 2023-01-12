using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;
    public static AudioManager Instance;
    private AudioMixer m_Mixer;
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
        m_AudioSource = GetComponent<AudioSource>();
        m_Mixer = GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
       
        
        m_Mixer.SetFloat("music", PlayerPrefs.GetFloat("music"));
        m_Mixer.SetFloat("Efx", PlayerPrefs.GetFloat("Efx"));
    }

    public void PlaySound(AudioClip clip)
    {
        m_AudioSource.PlayOneShot(clip);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetVolumeMixer(string name,float volume)
    {
        if (m_Mixer != null)
        {
            m_Mixer.SetFloat(name, volume);
        }
        PlayerPrefs.SetFloat(name,volume);
    }

    public void PauseAudio()
    {
        m_AudioSource.Pause();
    }

    public void ResumeAudio()
    {
        m_AudioSource.UnPause();
    }
}