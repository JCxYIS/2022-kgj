using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class OverlayUI : MonoBehaviour
{
    private static OverlayUI _instance;
    public static OverlayUI Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = Instantiate(Resources.Load<GameObject>("OverlayUI")).GetComponent<OverlayUI>();
            }
            return _instance;
        }
    }

    [Header("Bindings")]
    [SerializeField] RectTransform _lovePanel;
    [SerializeField] Text _loveCharaName;
    [SerializeField] Text _loveCharaValue;
    [SerializeField] Text _loveCharaAddValue;
    [SerializeField] RectTransform _achiPanel;
    [SerializeField] Text _achiText;



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _lovePanel.anchoredPosition = new Vector2(-300, -70.52f);
        _achiPanel.anchoredPosition = new Vector2(0, -300f);
        DontDestroyOnLoad(gameObject);
    }


    public void AddLove(string charaName, float addBefore, float addAfter)
    {
        float delta = addAfter - addBefore;
        _loveCharaName.text = charaName;
        _loveCharaValue.text = addBefore.ToString("0");
        _loveCharaAddValue.text = delta.ToString("0");
        _lovePanel.DOAnchorPosX(0, .39f).From(new Vector2(-300f, -70.52f)).OnComplete(()=>{
            StartCoroutine(AddLoveCoroutine(addBefore, addAfter));
        });
        ;
    }

    IEnumerator AddLoveCoroutine(float addBefore, float addAfter)
    {
        float delta = addAfter - addBefore;
        for(float t = 0; t < 1; t += Time.deltaTime / 0.75f)
        {
            _loveCharaValue.text = (addBefore + delta * t).ToString("0");
            _loveCharaAddValue.text = "+" + (addAfter - delta * t).ToString("0");
            yield return null;
        }
        _loveCharaValue.text = addAfter.ToString("0");
        _loveCharaAddValue.text = "+0" ;
        yield return new WaitForSeconds(1.5f);
        _lovePanel.DOAnchorPosX(-300f, .39f);
    }

    public void AchievementComplete(string achiname)
    {
        _achiText.text = achiname;
        _achiPanel.DOAnchorPosY(0, .39f).From(new Vector2(0f, -300)).OnComplete(()=>{
            _achiPanel.DOAnchorPosY(-300, .69f).SetDelay(3.5f);
        });        
    }
}