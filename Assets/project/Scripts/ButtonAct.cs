using DG.Tweening;
using UnityEngine;


public class ButtonAct : MonoBehaviour
{
 [SerializeField] 
 private AudioClip _audioClip;
 
 public void Click()
 {
  AudioManager.Instance.PlaySound(_audioClip);
  DOscaleinout(transform);
 }
  public void DOscaleinout(Transform transform)
   {
     transform.DOScale(0.5f, .1f)
       .SetEase(Ease.InCubic).OnComplete(()=>
         transform.DOScale(1f, .1f)
         .SetEase(Ease.InCubic));
   }
 
}
