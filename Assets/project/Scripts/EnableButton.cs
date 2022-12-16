using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableButton : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_AudioClip,errorEfx;

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
            AudioManager.Instance.PlaySound(m_AudioClip);
        }
        IsEnable = m_Button.IsInteractable();
    }

    public void PlaySound()
    {
        if (GameManager.Instance.checkSequence())
        {
            AudioManager.Instance.PlaySound(m_AudioClip);
        }
       
        else
        {
            AudioManager.Instance.PlaySound(errorEfx);
        }
        Debug.Log("Play sound");
    }
}
