using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    // [Header("Settings")]
    // public string[] Texts;

    [Header("Bindings")]
    [SerializeField] private CharacterSelectButton _modelPrefab;
    [SerializeField] private Image _charaArt;


    Character _currentCharacter;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _currentCharacter = GameManager.Instance.CurrentCharacter;
        SetCharaArt();

        Character[] characters = Resources.LoadAll<Character>("CharaSO");
        print("Loaded characters: " + characters.Length);
        foreach (var character in characters)
        {
            var model = Instantiate(_modelPrefab.gameObject, _modelPrefab.transform.parent.transform).GetComponent<CharacterSelectButton>();

            model.Name.text = character.Name;
            model.Love.text = "TODO"; // TODO
            model.Icon.sprite = character.GetIcon();
            var chara = character;
            model.Button.onClick.AddListener(() =>
            {
                _currentCharacter = chara;
                SetCharaArt();
            });
        }
        _modelPrefab.gameObject.SetActive(false);
    }

    public void SetCharaArt()
    {
        _charaArt.sprite = _currentCharacter.GetSprite();
    }

    public void ChooseCharacter()
    {
        GameManager.Instance.CurrentCharacter = _currentCharacter;
    }
}