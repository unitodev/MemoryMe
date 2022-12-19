using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject GameoverPanel,WaitPanel;
    public enum State
    {
        Show, Wait,Press,End
    }

    private void Start()
    {
        Instance = this;
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
                 text.text ="Level : "+ Level.ToString();
                 WaitPanel.SetActive(true);
                 break;
             case  State.Press:
                 WaitPanel.SetActive(false);
                 if (checkSequence()&& tempsequence.Count == currentsequence.Count)
                 {
                     CurrentState = State.Show;
                 }
                 break;
             case  State.End:
                 //show panel gameover
                 break;
        }
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
        if (CurrentState == State.Show)
        {
            tempsequence.Add(button);
        }
        else if(CurrentState == State.Press)
        {
            
            currentsequence.Add(button);
            if (!checkSequence() )
            {
                GameoverPanel.SetActive(true);
                CurrentState = State.End;
            }
        }
    }
}
