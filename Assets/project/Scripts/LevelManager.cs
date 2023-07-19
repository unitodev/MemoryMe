using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
   public static LevelManager Instance;
    [SerializeField] 
   private Transform _pos;

   private Vector3 _startpos;
    [SerializeField] 
   private Ease ease;

    [SerializeField] 
   private float _duration;

   public UnityEvent OnTransitionEnd;
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
      _startpos = _pos.position;
     
   }

   public void LoadScene(int index)
   {
      var sequence = DOTween.Sequence();
      
      var sceneAsync= SceneManager.LoadSceneAsync(index);
      sceneAsync.allowSceneActivation = false;
      sequence.Append(_pos.DOLocalMove(Vector3.zero, _duration).SetEase(ease).OnComplete(() =>
         {
            sceneAsync.allowSceneActivation = true;
         })
      );
      sequence.Append(_pos.DOLocalMoveX(_startpos.x, _duration).SetEase(ease)).OnComplete(() => OnTransitionEnd.Invoke());
   }
}
