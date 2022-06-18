using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;

public class StoryController : MonoBehaviour
{
    public Flowchart _flowchart;

    void Start()
    {
        // _flowchart.FindBlock("Undone_PostGame").StartExecution();

        // Determine story to play

        // end story
        if(GameManager.Instance.IsEndedFromMainGame)
        {
            GameManager.Instance.IsEndedFromMainGame = false;

            switch(GameManager.Instance.CurrentCharacter.name)
            {
                case "00.Alco":
                    _flowchart.FindBlock("PostGame").StartExecution();
                break;
                default:
                    Debug.LogWarning("角色結尾故事尚未完成: "+ GameManager.Instance.CurrentCharacter.name);
                    _flowchart.FindBlock("Undone_PostGame").StartExecution();
                    break;
            }

            return;
        }
        
        // Start Story
        switch(GameManager.Instance.CurrentCharacter.name)
        {
            case "00.Alco":
                _flowchart.FindBlock("Intro").StartExecution();
                break;
            default:
                Debug.LogWarning("角色故事尚未完成 :"+GameManager.Instance.CurrentCharacter.name);
                _flowchart.FindBlock("Undone").StartExecution();
                break;
        }
    }
}