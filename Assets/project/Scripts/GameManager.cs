using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
     
    public State CurrentState;
     [SerializeField]
    private List<Button> _buttonList;
   [SerializeField]
    private List<Button> _currentsequence;
     [SerializeField]
    private List<Button> _tempsequence;

    public static GameManager Instance;

     [SerializeField] 
    private int _level = 1;

   

      [SerializeField] 
    private Transform _timeImage;
     [SerializeField]
    private GameObject _gameoverPanel;

     [SerializeField]
    private GameObject _waitPanel;

    [SerializeField] 
    private Transform _gameoverpanelFront;
    [Header("Text")]
    [SerializeField] 
    private TextMeshProUGUI _leveltext;
   [SerializeField] 
    private TextMeshProUGUI _scoretext;

     [SerializeField] 
    private TextMeshProUGUI _HscoreText;

    [SerializeField] 
    private TextMeshProUGUI stateText;

    public bool isPause = false;
    private int heightScore = 0;
    public InterstitialAdExample AdExample;
    public enum State
    {
        Show, Wait,Press,End
    }

    private void Start()
    {
        Instance = this;
        heightScore = PlayerPrefs.GetInt("memoryme-hightScore");
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
             case State.Show:
                 AdExample.LoadAd();
                     RandomButton();
                 
                 //show current button
               showSequence().GetAwaiter().OnCompleted(()=>CurrentState=State.Press);
                     CurrentState = State.Wait;
                 break;
             case State.Wait:
                 _leveltext.text = _level.ToString();
                 _waitPanel.SetActive(true);
                 break;
             case  State.Press:
                 _waitPanel.SetActive(false);
                 

                 if (_timeImage.localScale.x > 0&&!isPause)
                 {
                     _timeImage.localScale =new Vector3(_timeImage.localScale.x-Time.deltaTime*0.25f,1,1);
                 }
                 else if(_timeImage.localScale.x <= 0)
                 {
                     GameOver();
                     CurrentState = State.End;
                 }
                 break;
             case  State.End:
                 _scoretext.text = _level.ToString();
                 _HscoreText.text = heightScore.ToString();
                 if (_level >= heightScore)
                 {
                     _HscoreText.text = _level.ToString();
                     //saveHscore
                     PlayerPrefs.SetInt("memoryme-hightScore",_level);
                 }
                 break;
        }

        stateText.text = CurrentState.ToString();
    }

   public bool checkSequence()
    {
        bool correct = false;
       foreach (var index in _currentsequence.Select((button, i) =>new {button,i} ))
       {
           if (index.button == _tempsequence[index.i])
                correct = true;
           else
           {
               correct = false;
           }
       }

       return correct;
    }

    async Task showSequence()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        foreach (var button in _tempsequence)
        {
            await Task.Delay(TimeSpan.FromSeconds(.5));
            button.interactable = false;
            await Task.Delay(TimeSpan.FromSeconds(.5));
            button.interactable = true;
            await Task.Delay(TimeSpan.FromSeconds(.5));
        }

        await Task.Yield();
    }
    void RandomButton()
    {
       var index= Random.Range(0,_buttonList.Count);
      
      
           Addtosequence(_buttonList[index]);
           _level = _tempsequence.Count;
        _currentsequence.Clear();
    }
    public void Addtosequence(Button button)
    {
        _timeImage.localScale = Vector3.one;
        if (CurrentState == State.Show)
        {
            _tempsequence.Add(button);
        }
        else if(CurrentState == State.Press)
        {
            
            _currentsequence.Add(button);
            if (!checkSequence() )
            {
                GameOver();
            }
            if (checkSequence()&& _tempsequence.Count == _currentsequence.Count)
            {
                CurrentState = State.Show;
            }
        }
    }

    private void GameOver()
    {
        
        AdExample.ShowAd();
        _gameoverPanel.SetActive(true);
        _gameoverpanelFront.DOScale(1, .5f).SetEase(Ease.InCubic);
        CurrentState = State.End;
       
    }
    public void Reset()
    {
        CurrentState = State.Show;
        _level = 1;
        _tempsequence.Clear();
    }

    public void tutorial()
    {
        isPause = true;
    }
}
