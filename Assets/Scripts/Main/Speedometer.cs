using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [Header("Settings")]
    // [Range(0, 100)]
    public float ScrollSensitivity = 30f;

    [Header("Bindings")]
    [SerializeField] Slider slider;
    [SerializeField] Text text;

    [Header("Runtime")]

    public bool IsSpeedLockActive = false;
    public float Speed => speed;
    float speed = 0;





    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        slider.value = 0;
        text.text = "0";
        slider.onValueChanged.AddListener(SliderValueChanged);
    }
    

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        var scrollData = Input.GetAxis("Mouse ScrollWheel");
        // print(scrollData);

        speed += scrollData * ScrollSensitivity * 100 * Time.deltaTime;

        if(speed < -15f) 
            speed = -15f;
        else if(speed >= 100)
            speed = 100;

        if(IsSpeedLockActive)
        {
            if(speed < -3f) 
                speed = -3f;
            else if(speed >= 3)
                speed = 3;
        }

        slider.value = speed / 100;
        text.text = speed.ToString("0");
        text.color = IsSpeedLockActive ? Color.red : Color.black;
    }

    void SliderValueChanged(float value)
    {
        speed = value * 100;
    }
    
}
