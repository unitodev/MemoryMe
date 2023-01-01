using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FunctionalButton : MonoBehaviour
{
   public enum buttontype
    {
        restart,home,close,setting,play
    }

   [SerializeField]
    private buttontype Button;

    [SerializeField] private Transform blackpanel,frontpanel;

    public Ease Ease;
    // Start is called before the first frame update
    public void Onclick()
    {
        switch (Button)
        {
            case buttontype.restart: frontpanel.DOScale(0, 1f).SetEase(Ease).OnComplete(() =>
            {
                blackpanel.gameObject.SetActive(false);
                GameManager.Instance.Reset();
            });
                break;
            case buttontype.close: frontpanel.DOScale(0, 1f).SetEase(Ease).OnComplete(() =>
                { if(GameManager.Instance!=null)
                    GameManager.Instance.isPause = false;
                    blackpanel.gameObject.SetActive(false);
                });
                break;
            case buttontype.home: frontpanel.DOScale(0, 1f).SetEase(Ease).OnComplete(() =>
                {
                   LevelManager.Instance.LoadScene(0);
                });
                break;
            case buttontype.setting:
                if(GameManager.Instance!=null)
                GameManager.Instance.isPause = true;
                blackpanel.gameObject.SetActive(true); 
                Show(frontpanel);
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
