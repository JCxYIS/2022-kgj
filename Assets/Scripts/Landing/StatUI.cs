using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [Header("Bindings")]
    public GameObject _model;

    class Stat
    {
        public string key;
        public string desc;
        public string unit;

        public Stat(string key, string desc, string unit)
        {
            this.key = key;
            this.desc = desc;
            this.unit = unit;
        }
    }

    Stat[] statList = new Stat[] { 
        new Stat("STAT_GamePlayed",  "遊戲次數", "次"),
        new Stat("STAT_GameTime",  "遊戲時間", "秒"),
        new Stat("STAT_Laps", "總繞行圈數", "圈"),
        new Stat("STAT_KM", "總行駛距離", "m"),
        new Stat("STAT_BestLapTime",  "最快繞行時間", "秒"),
        new Stat("STAT_Collide",  "總撞車次數", "次"),
        new Stat("STAT_TOTAL_LOVE",  "總好感度", ""),
    };


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        for(int i = 0; i < statList.Length; i++)
        {
            CreateStat(statList[i].key, statList[i].desc, statList[i].unit);
        }
        _model.gameObject.SetActive(false);
    }

    void CreateStat(string key, string name, string unit)
    {
        var g = Instantiate(_model, _model.transform.parent.transform);
        g.transform.GetChild(0).GetComponent<Text>().text = name;
        g.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetFloat(key, 0).ToString("F2") + " " + unit;
    }
}