using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
                var charaName = PlayerPrefs.GetString("MyCharacter", "00.Stone");
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

    protected override void Init()
    {
        
        DontDestroyOnLoad(gameObject);        
    }
    

    public void MainGameEnded(int checkpointPassed, float time)
    {
        Debug.Log($"Main game ended. Checkpoint passed: {checkpointPassed}, time: {time}");
    }
}