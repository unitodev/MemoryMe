using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class introUI : MonoBehaviour
{
    [SerializeField] private List<Transform> _items;
    [SerializeField] private List<Vector3> _pos;

    [SerializeField] private Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        savepos();
        introMove();
        LevelManager.Instance.OnTransitionEnd.AddListener(introMove);
    }

    void savepos()
    {
       
        foreach (var VARIABLE in _items)
        {
             _pos.Add(VARIABLE.position);
            
            VARIABLE.position=new Vector3(0,20);
        }
    }
    [ContextMenu("move")]
    void introMove()
    {
        foreach (var VARIABLE in _items.Select((value,i)=>(value,i)))
        {
             VARIABLE.value.DOMove(_pos[VARIABLE.i],.5f).SetEase(ease).SetDelay(VARIABLE.i*0.2f);
        }
    }

    void introScale()
    {
        foreach (var VARIABLE in _items)
        {
             VARIABLE.DOScale(Vector3.one, .5f).SetEase(ease).Delay();
        }
    }
    private void OnDestroy()
    {
        LevelManager.Instance.OnTransitionEnd.RemoveListener(introMove);
    }
}
