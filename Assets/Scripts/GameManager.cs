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
        Application.targetFrameRate = 60;
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

        // Add love
        int lap = checkpointPassed / 8;
        float avTime = time / lap;
        float bonus = 0;        
        if(avTime <= 20)
            bonus = lap * 8.7878f;
        else if(avTime <= 25)
            bonus = lap * 4.8763f;
        else if(avTime <= 30)
            bonus = lap * 3.1415f;
        else if(avTime <= 40)
            bonus = lap * 2.020f;
        else if(avTime <= 60)
            bonus = lap * 0.8763f;
        
        AddLovePoint(CurrentCharacter, (lap * 3f + bonus + checkpointPassed/12) * CurrentCharacter.LovePointMultiplier);

        PlayerPrefs.SetFloat("STAT_GamePlayed", PlayerPrefs.GetFloat("STAT_GamePlayed", 0) + 1);
        PlayerPrefs.SetFloat("STAT_GameTime", PlayerPrefs.GetFloat("STAT_GameTime", 0) + time);
        PlayerPrefs.SetFloat("STAT_Laps", PlayerPrefs.GetFloat("STAT_Laps", 0) + lap);
        PlayerPrefs.SetFloat("STAT_KM", PlayerPrefs.GetFloat("STAT_KM", 0) + checkpointPassed * 628f / 8f);


        //
        PlayerPrefs.Save();
    }

    /* -------------------------------------------------------------------------- */

    public float GetLovePoint(Character character)
    {
        float lp = PlayerPrefs.GetFloat("Love_"+character.name, 0);

        return lp;
    }

    public float AddLovePoint(Character character, float amount)
    {
        float lp = PlayerPrefs.GetFloat("Love_"+character.name, 0);
        lp += amount;
        if(lp < 0)
        {
            lp = 0;
        }
        if(lp > 100)
        {
            lp = 100;
        }
        PlayerPrefs.SetFloat("Love_"+character.name, lp);
        OverlayUI.Instance.AddLove(CurrentCharacter.Name, lp - amount, lp);
        return lp;
    }
    /* -------------------------------------------------------------------------- */
    
    public void AchievementComplete(string achiName)
    {
        PlayerPrefs.SetFloat(achiName, 1);
        OverlayUI.Instance.AchievementComplete(AchievementUI.GetAchiByKey(achiName).title);
    }

}