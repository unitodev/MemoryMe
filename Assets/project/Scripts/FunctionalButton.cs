using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class FunctionalButton : MonoBehaviour
{
   public enum buttontype
    {
        restart,home,close,setting,play
    }

     [SerializeField]
    private buttontype _button;

     [SerializeField] private Transform _blackpanel;
     [SerializeField] private Transform _frontpanel;

    public Ease Ease;
    // Start is called before the first frame update
    public void Onclick()
    {
        switch (_button)
        {
            case buttontype.restart: _frontpanel.DOScale(0, 1f).SetEase(Ease).OnComplete(() =>
            {
                _blackpanel.gameObject.SetActive(false);
                GameManager.Instance.Reset();
            });
                break;
            case buttontype.close: _frontpanel.DOScale(0, 1f).SetEase(Ease).OnComplete(() =>
                { if(GameManager.Instance!=null)
                    GameManager.Instance.isPause = false;
                    _blackpanel.gameObject.SetActive(false);
                });
                break;
            case buttontype.home: _frontpanel.DOScale(0, 1f).SetEase(Ease).OnComplete(() =>
                {
                   LevelManager.Instance.LoadScene(0);
                });
                break;
            case buttontype.setting:
                if(GameManager.Instance!=null)
                GameManager.Instance.isPause = true;
                _blackpanel.gameObject.SetActive(true); 
                Show(_frontpanel);
                break;
            case buttontype.play:
                LevelManager.Instance.LoadScene(1);
          
                break;
                
        }
    }
    public void Show(Transform transform)
    {
        transform.DOScale(1, .5f).SetEase(Ease);
    }
}
