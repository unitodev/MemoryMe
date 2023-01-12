using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
   public static LevelManager Instance;
   [SerializeField] 
   private Transform pos;

   private Vector3 startpos;
   [SerializeField] 
   private Ease Ease;

   [SerializeField] 
   private float duration;
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
      startpos = pos.position;
     
   }

   public void LoadScene(int index)
   {
      var sequence = DOTween.Sequence();
      
      var sceneAsync= SceneManager.LoadSceneAsync(index);
      sceneAsync.allowSceneActivation = false;
      sequence.Append(pos.DOLocalMove(Vector3.zero, duration).SetEase(Ease).OnComplete(() =>
         {
            sceneAsync.allowSceneActivation = true;
         })
      );
      sequence.Append(pos.DOLocalMoveX(startpos.x, duration).SetEase(Ease));
   }
}
