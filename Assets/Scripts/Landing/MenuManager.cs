using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [Header("Settings")]
    public List<CanvasGroup> _panels; // 0: Main, 1: Sub, 2: SubSub, etc
    public OpenMenuChannel _openMenuChannel;

    // [Header("Bindings")]

    CanvasGroup _currentPanel;



    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _openMenuChannel.OnEventRaised += OpenPanel;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {        
        _openMenuChannel.OnEventRaised -= OpenPanel;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < _panels.Count; i++)
        {
            _panels[i].gameObject.SetActive(false);
        }
        OpenPanel(0);

        if(PlayerPrefs.GetFloat("STAT_GamePlayed", 0) == 0)
        {
            GoGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel(int index)
    {
        if(_currentPanel != null)
        {
            var tmpPanel = _currentPanel;
            tmpPanel.transform.DOScale(.8f, .2f);
            tmpPanel.DOFade(0, .2f).OnComplete(() =>
            {
                tmpPanel.gameObject.SetActive(false);
            });
        }

        _currentPanel = _panels[index];
        _currentPanel.gameObject.SetActive(true);
        _currentPanel.transform.DOScale(1.0f, .2f).From(Vector3.one * .8f);
        _currentPanel.DOFade(1, .2f).From(0);

        //  OverlayUI.Instance.AddLove(GameManager.Instance.CurrentCharacter.Name, 10, 87);
    }

    public void GoGame()
    {
        GameManager.Instance.GoGame();
    }
}
