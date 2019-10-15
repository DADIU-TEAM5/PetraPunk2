using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class HighScoreVariable : ScriptableObject
{
    public string[] names;
    public int[] scores;
    public int[] collectibles;
}
