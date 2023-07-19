
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnableButton : MonoBehaviour
{ 
    [SerializeField]
    private AudioClip _mAudioClip;

   [SerializeField]
    private AudioClip _correctEfx;

   [SerializeField]
    private AudioClip _errorEfx;

    [SerializeField] 
    private ParticleSystem _particleSystem;
    private Button _button;

    private bool _isEnable;


    // Start is called before the first frame update
    private void Start()
    {
        _button = GetComponent<Button>();
        _isEnable = _button.IsInteractable();
    }

    private void FixedUpdate()
    {
        
        
        if (_isEnable!=_button.IsInteractable()&&!_button.IsInteractable())
        {
            AudioManager.Instance.PlaySound(_mAudioClip);
            DOscaleinout(_button.transform,.5f,1f);
        }
        _isEnable = _button.IsInteractable();
        
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
        DOscaleinout(_button.transform,.5f,1f);
        if (!GameManager.Instance.checkSequence())
        {
            AudioManager.Instance.PlaySound(_errorEfx);
            return;
        }
        if (GameManager.Instance.CurrentState!=GameManager.State.End)
        {
            _particleSystem.transform.DOMove(transform.position,0f);
            _particleSystem.Play();
            AudioManager.Instance.PlaySound(_mAudioClip);
            AudioManager.Instance.PlaySound(_correctEfx);
        }
    }
}
