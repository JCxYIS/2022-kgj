using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [Header("Bindings")]
    [SerializeField] private Image _charaArt;
    [SerializeField] private Text _love;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _charaArt.sprite = GameManager.Instance.CurrentCharacter.GetSprite();
        _love.text = GameManager.Instance.GetLovePoint(GameManager.Instance.CurrentCharacter).ToString("0");
    }
}