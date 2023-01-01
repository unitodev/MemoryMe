using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EnableButton : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_AudioClip,errorEfx;

    private Button m_Button;

    private bool IsEnable;
    private Light2D _light2D;

    // Start is called before the first frame update
    private void Start()
    {
        m_Button = GetComponent<Button>();
        IsEnable=m_Button.IsInteractable();
        _light2D = GetComponentInChildren<Light2D>();
    }

    private void FixedUpdate()
    {
        
        
        if (IsEnable!=m_Button.IsInteractable()&&!m_Button.IsInteractable())
        {
            AudioManager.Instance.PlaySound(m_AudioClip);
            DOscaleinout(m_Button.transform,.5f,1f);



        }
        IsEnable = m_Button.IsInteractable();
    }
    public void DOscaleinout(Transform transform,float start,float to)
    {
        transform.DOScale(start, .1f)
            .SetEase(Ease.InCubic).OnComplete(()=>
                transform.DOScale(to, .1f)
                    .SetEase(Ease.InCubic));
    }

    public void PlaySound()
    { 
        DOscaleinout(m_Button.transform,.5f,1f);
        if (!GameManager.Instance.checkSequence())
        {
            AudioManager.Instance.PlaySound(errorEfx);
            return;
        }
        if (GameManager.Instance.CurrentState!=GameManager.State.End)
        {
            AudioManager.Instance.PlaySound(m_AudioClip);
        }
    }
}
