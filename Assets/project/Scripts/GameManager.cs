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
    private State CurrentState;
    [SerializeField]
    private List<Button> buttonList;
    [SerializeField]
    private List<Button> currentsequence;
    [SerializeField]
    private List<Button> tempsequence;

    

    [SerializeField] 
    private int Level = 1;

    [SerializeField] 
    private TextMeshProUGUI text;
    public enum State
    {
        Show, Wait,Press,End
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
                 break;
             case  State.Press:
                 if (checkSequence()&& tempsequence.Count == currentsequence.Count)
                 {
                     CurrentState = State.Show;
                 }
                 break;
             case  State.End:
                 break;
        }
    }

    bool checkSequence()
    {
       // tempsequence;
       // currentsequence;
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
                CurrentState = State.End;
            }
        }
    }
}
