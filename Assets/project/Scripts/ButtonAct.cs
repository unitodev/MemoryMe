using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonAct : MonoBehaviour
{
 [SerializeField] 
 private AudioClip _audioClip;
 
 public void Click()
 {
  AudioManager.Instance.PlaySound(_audioClip);
 }

 public void LoadScene(int index)
 {
  LevelManager.Instance.LoadScene(index);
 }
}
