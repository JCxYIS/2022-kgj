using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchiCompleteCaller : MonoBehaviour
{
    public void Call(string achiName)
    {
        GameManager.Instance.AchievementComplete(achiName);
    }
}