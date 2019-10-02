using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class SaveData 
{
    public int StoryModeCompleted;


    public int[] scores;
    public int[] collectibles;
    public string[] names;

    public SaveData(highScoreVariable highscore)
    {

        scores = new int[ highscore.scores.Length];
        collectibles = new int[highscore.collectibles.Length];
        names = new string[ highscore.names.Length];
    }

}
