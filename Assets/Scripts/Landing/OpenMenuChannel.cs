using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "OpenMenuChannel", menuName = "Channels/OpenMenuChannel")]
public class OpenMenuChannel : ScriptableObject
{
    /// <summary>
    /// 收聽這個 Channel。
    /// 不要直接 Invoke! 請使用 <see langword="RaiseEvent()"/>
    /// </summary>
    public event UnityAction<int> OnEventRaised;

    /// <summary>
    /// 呼叫這個 channel。
    /// </summary>
    public void RaiseEvent(int channelIndex)
    {
        if(OnEventRaised != null)
        {
            OnEventRaised?.Invoke(channelIndex);
        }
        else
        {
            Debug.Log("成功接通 OpenMenuChannel，但是沒有人收聽...");
        }
    }
}