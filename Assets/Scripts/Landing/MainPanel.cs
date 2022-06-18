using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [Header("Bindings")]
    [SerializeField] private Image _charaArt;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _charaArt.sprite = GameManager.Instance.CurrentCharacter.GetSprite();
    }
}