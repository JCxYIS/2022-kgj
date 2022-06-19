using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class MainPanel : MonoBehaviour
{
    [Header("Bindings")]
    [SerializeField] private Image _charaArt;
    [SerializeField] private Text _love;

    [SerializeField] private RectTransform _panel0;
    [SerializeField] private RectTransform _panel1;
    [SerializeField] private RectTransform _panel2;
    [SerializeField] private RectTransform _panel3;

    Vector2 _panel0Pos = Vector2.zero;
    Vector2 _panel1Pos;
    Vector2 _panel2Pos;
    Vector2 _panel3Pos;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        if(_panel0Pos == Vector2.zero)
        {
            _panel0Pos = _panel0.anchoredPosition;
            _panel1Pos = _panel1.anchoredPosition;
            _panel2Pos = _panel2.anchoredPosition;
            _panel3Pos = _panel3.anchoredPosition;
        }

        _charaArt.sprite = GameManager.Instance.CurrentCharacter.GetSprite();
        _love.text = GameManager.Instance.GetLovePoint(GameManager.Instance.CurrentCharacter).ToString("0");

        _panel0.DOAnchorPosX(_panel0Pos.x, 0.5f).From(_panel0Pos + Vector2.left * 300f);
        _panel1.DOAnchorPosX(_panel1Pos.x, 0.5f).From(_panel1Pos + Vector2.right * 300f);
        _panel2.DOAnchorPosY(_panel2Pos.y, 0.5f).From(_panel2Pos + Vector2.up * 100f);
        _panel3.DOAnchorPosY(_panel3Pos.y, 0.5f).From(_panel3Pos + Vector2.down * 100f);
    }
}