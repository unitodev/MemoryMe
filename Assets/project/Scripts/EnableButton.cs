using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableButton : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_AudioClip;

    private Button m_Button;

    private bool IsEnable;
    // Start is called before the first frame update
    private void Start()
    {
        m_Button = GetComponent<Button>();
        IsEnable=m_Button.IsInteractable();
    }

    private void FixedUpdate()
    {
        
        if (IsEnable!=m_Button.IsInteractable()&&!m_Button.IsInteractable())
        {
            PlaySound();
        }
        IsEnable = m_Button.IsInteractable();
    }

    public void PlaySound()
    {
        AudioManager.Instance.PlaySound(m_AudioClip);
        Debug.Log("Play sound");
    }
}
