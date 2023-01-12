using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScaleLoop : MonoBehaviour
{
    [SerializeField] private bool isScale, isRotate,isMove;

    [SerializeField] private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        if(isScale)
        DOscaleLoop(transform);
        if (isRotate)
            DOrotateLoop(transform);
        if(isMove)
            DoMoveLoop();
    }

    public void DOscaleLoop(Transform transform)
    {
        transform.DOScale(.5f, .5f).SetEase(Ease.InCubic).SetLoops(-1, LoopType.Yoyo).SetDelay(.2f);
       
    }
    public void DOrotateLoop(Transform transform)
    {
        Vector3 axis = new Vector3(0, 0, 1);
        transform.DORotate(axis*10, .5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        
    }

    public void DoMoveLoop()
    {
        transform.DOMove(transform.position+pos, .5f).SetEase(Ease.InCubic).SetLoops(-1, LoopType.Yoyo);
    }
}
