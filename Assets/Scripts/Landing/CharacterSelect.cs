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
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Character[] characters = Resources.LoadAll<Character>("CharaSO");
        print("Loaded characters: " + characters.Length);
        float totalLove = 0;
        foreach (var character in characters)
        {
            var model = Instantiate(_modelPrefab.gameObject, _modelPrefab.transform.parent.transform).GetComponent<CharacterSelectButton>();

            model.Name.text = character.Name;
            model.Love.text = GameManager.Instance.GetLovePoint(character).ToString("0"); 
            model.Icon.sprite = character.GetIcon();
            var chara = character;
            model.Button.onClick.AddListener(() =>
            {
                _currentCharacter = chara;
                SetCharaArt();
            });
            totalLove += GameManager.Instance.GetLovePoint(character);
        }
        PlayerPrefs.SetFloat("STAT_TOTAL_LOVE", totalLove);
        _modelPrefab.gameObject.SetActive(false);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _currentCharacter = GameManager.Instance.CurrentCharacter;
        SetCharaArt();        
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