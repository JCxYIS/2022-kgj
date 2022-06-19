using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    [Header("Bindings")]
    public GameObject _model;

    class Achi
    {
        public string key;
        public float requirement;
        public string title;
        public string desc;

        public Achi(string key, float requirement, string title, string desc)
        {
            this.key = key;
            this.requirement = requirement;
            this.title = title;
            this.desc = desc;
        }
    }

    Achi[] achiList = new Achi[] { 
        new Achi("ACHI_03", 1, "憑實力單身", "遊戲的結局中，主角最後是單身"),
        new Achi("ACHI_04", 1, "我單推", "遊戲中只有一位女主角"),
        new Achi("ACHI_05", 1, "開玩笑~我超勇的好不好", "遊戲中出現酒"),
        new Achi("ACHI_06", 1, "你才王安石，你全家都王安石", "遊戲文字和遊戲內容差異很大"),
        new Achi("ACHI_07", 1, "求報價", "遊戲中途會莫名彈出對話訊息請你報價"),
        new Achi("ACHI_08", 1, "連我阿嬤都會玩", "遊戲中僅使用一顆按鍵即可遊玩！我阿嬤說滑鼠不算按鍵"),
        new Achi("ACHI_08A", 1, "連我阿嬤都會玩．改", "只使用一個按鍵 (左鍵、右鍵、中鍵擇一) 就完成一圈"),
        new Achi("STAT_Laps", 100, "戀愛循環", "總行駛圈數超過 100 圈"),
        new Achi("STAT_KM", 30000, "超跑情人夢", "總行駛距離超過 30 公里"),
        new Achi("STAT_Collide", 487, "碰碰車", "撞車次數達到 487 "),
        new Achi("ACHI_DEJA_VU", 1, "Deja Vu", "在 15 秒內達成一圈"),
    };


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        for(int i = 0; i < achiList.Length; i++)
        {
            CreateStat(achiList[i]);
        }
        _model.gameObject.SetActive(false);
    }

    void CreateStat(Achi achi)
    {
        float value = PlayerPrefs.GetFloat(achi.key, 0);
        var g = Instantiate(_model, _model.transform.parent.transform);
        g.transform.GetChild(0).GetComponent<Text>().text = achi.title;
        g.transform.GetChild(1).GetComponent<Text>().text = achi.desc;
        g.transform.GetChild(2).GetComponent<Slider>().value = value / achi.requirement;
        g.transform.GetChild(3).GetComponent<Text>().text = value.ToString("0") + "/" + achi.requirement;
    }
}