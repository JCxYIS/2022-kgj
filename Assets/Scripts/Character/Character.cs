using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character")]
public class Character : ScriptableObject
{
    public string Name;
    public float LovePointMultiplier = 1f;
    public Sprite Sprite;
    public AudioClip OverrideEngineSound;
}