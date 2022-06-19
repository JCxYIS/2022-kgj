using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] MainStoryController mainStoryController;
    


    [Header("Runtime")]
    public float time;
    public bool isRunning;



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(isRunning && !mainStoryController.IsStoryShowing)
        {
            time += Time.deltaTime;
        }

        text.text = $"TIME: {((int)time/60).ToString("00")}:{(time%60).ToString("00")}.{(time*100%100).ToString("00")}";
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}