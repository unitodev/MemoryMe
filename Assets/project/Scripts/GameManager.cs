using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    public State CurrentState;
    [SerializeField]
    private List<Button> buttonList;
    [SerializeField]
    private List<Button> currentsequence;
    [SerializeField]
    private List<Button> tempsequence;

    public static GameManager Instance;

    [SerializeField] 
    private int Level = 1;

    [SerializeField] 
    private TextMeshProUGUI Leveltext,stateText;

    [SerializeField] 
    private Transform TimeImage;
    [SerializeField]
    private GameObject GameoverPanel,WaitPanel;

    [SerializeField] 
    private Transform GameoverpanelFront;
    [SerializeField] 
    private TextMeshProUGUI Scoretext,HScoreText;
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

                     RandomButton();
                 
                 //show current button
               showSequence().GetAwaiter().OnCompleted(()=>CurrentState=State.Press);
                     CurrentState = State.Wait;
                 break;
             case State.Wait:
                 Leveltext.text ="Level : "+ Level.ToString();
                 WaitPanel.SetActive(true);
                 break;
             case  State.Press:
                 WaitPanel.SetActive(false);
                 

                 if (TimeImage.localScale.x > 0&&!isPause)
                 {
                     TimeImage.localScale =new Vector3(TimeImage.localScale.x-Time.deltaTime*0.25f,1,1);
                 }
                 else if(TimeImage.localScale.x <= 0)
                 {
                     GameOver();
                     CurrentState = State.End;
                 }
                 break;
             case  State.End:
                 Scoretext.text = Level.ToString();
                 HScoreText.text = heightScore.ToString();
                 if (Level >= heightScore)
                 {
                     HScoreText.text = Level.ToString();
                     //saveHscore
                     PlayerPrefs.SetInt("memoryme-hightScore",Level);
                 }
                 break;
        }

        stateText.text = CurrentState.ToString();
    }

   public bool checkSequence()
    {
        bool correct = false;
       foreach (var index in currentsequence.Select((button, i) =>new {button,i} ))
       {
           if (index.button == tempsequence[index.i])
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
        foreach (var button in tempsequence)
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
       var index= Random.Range(0,buttonList.Count);
      
      
           Addtosequence(buttonList[index]);
           Level = tempsequence.Count;
        currentsequence.Clear();
    }
    public void Addtosequence(Button button)
    {
        TimeImage.localScale = Vector3.one;
        if (CurrentState == State.Show)
        {
            tempsequence.Add(button);
        }
        else if(CurrentState == State.Press)
        {
            
            currentsequence.Add(button);
            if (!checkSequence() )
            {
                GameOver();
            }
            if (checkSequence()&& tempsequence.Count == currentsequence.Count)
            {
                CurrentState = State.Show;
            }
        }
    }

    private void GameOver()
    {
        AdExample.LoadAd();
        GameoverPanel.SetActive(true);
        GameoverpanelFront.DOScale(1, .5f).SetEase(Ease.InCubic);
        CurrentState = State.End;
       
    }
    public void Reset()
    {
        CurrentState = State.Show;
        Level = 1;
        tempsequence.Clear();
    }
}
