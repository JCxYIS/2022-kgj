using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [Header("Bindings")]
    public GameObject _model;


    string[] statList = new string[] { 
        "STAT_GamePlayed", 
        "STAT_GameTime", 
        "STAT_Laps",
        "STAT_KM",
        "STAT_BestLapTime" 
    };


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        for(int i = 0; i < statList.Length; i++)
        {
            CreateStat(statList[i], statList[i]);
        }
        _model.gameObject.SetActive(false);
    }

    void CreateStat(string name, string key)
    {
        var g = Instantiate(_model, _model.transform.parent.transform);
        g.transform.GetChild(0).GetComponent<Text>().text = name;
        g.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString(key, "0");
    }
}