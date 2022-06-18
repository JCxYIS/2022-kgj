using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);        
    }

    public void MainGameEnded(int checkpointPassed, float time)
    {
        Debug.Log($"Main game ended. Checkpoint passed: {checkpointPassed}, time: {time}");
    }
}