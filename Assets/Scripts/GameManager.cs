using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    /* -------------------------------------------------------------------------- */


    private Character _currentCharacter;

    public Character CurrentCharacter
    {
        get
        {
            if(_currentCharacter == null)
            {
                var charaName = PlayerPrefs.GetString("MyCharacter", "00.Alco");
                _currentCharacter = Resources.Load<Character>("CharaSO/"+charaName);            
            }
            return _currentCharacter;
        }
        set
        {
            _currentCharacter = value;
            PlayerPrefs.SetString("MyCharacter", value.name);
        }
    }
    
    /* -------------------------------------------------------------------------- */

    public bool IsEndedFromMainGame = false;

    /* -------------------------------------------------------------------------- */

    protected override void Init()
    {
        
        DontDestroyOnLoad(gameObject);        
    }
    
    public void GoGame()
    {
        SceneManager.LoadScene("Story");
        PlayerPrefs.Save();
    }

    public void MainGameEnded(int checkpointPassed, float time)
    {
        Debug.Log($"Main game ended. Checkpoint passed: {checkpointPassed}, time: {time}");
        IsEndedFromMainGame = true;
        SceneManager.LoadScene("Story");
        PlayerPrefs.Save();
    }
}