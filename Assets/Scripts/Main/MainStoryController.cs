using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using DG.Tweening;

public class MainStoryController : MonoBehaviour
{
    [Header("Bindings")]
    [SerializeField] Flowchart _flowchart;
    [SerializeField] CanvasGroup _stage;

    int instructionOrder = 0;
    public bool IsStoryShowing = false;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _stage.alpha = 0;
    }

    public void StartInstruction(int i)
    {
        if(instructionOrder != i-1)
        {
            Debug.LogWarning("Instruction 不須啟動");
            return;
        }

        print("StartInstruction: " + i);        
        instructionOrder = i;

        StartStory("Instruction" + i);
    }

    public void StartStory(string block)
    {
        _flowchart.FindBlock(block).StartExecution();
        _stage.DOFade(1, .39f).From(0);
        IsStoryShowing = true;
    }

    public void StoryEnd()
    {
        _stage.DOFade(0, .39f).From(1);
        IsStoryShowing = false;
    }
}