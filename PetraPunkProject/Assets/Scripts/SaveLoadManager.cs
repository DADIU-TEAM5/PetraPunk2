using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveData saveData;
    //public SaveData saveData;
    public static highScoreVariable highScore;
    public highScoreVariable highScoreStart;

    // Start is called before the first frame update

    private void Awake()
    {
        highScore = highScoreStart;
        LoadData();
    }

    public static void SaveData()
    {
        for (int i = 0; i < highScore.scores.Length; i++)
        {
            PlayerPrefs.SetString("Names" + i, highScore.names[i]);
            PlayerPrefs.SetInt("Score" + i, highScore.scores[i]);
            PlayerPrefs.SetInt("collectibles" + i, highScore.collectibles[i]);

        }


        PlayerPrefs.SetInt("story_Mode_Completed", saveData.StoryModeCompleted);
        PlayerPrefs.Save();
    }

    public static void LoadData()
    {
        SaveData loadedData = new SaveData();


        for (int i = 0; i < highScore.scores.Length; i++)
        {
            PlayerPrefs.GetString("Names" + i, highScore.names[i]);
            PlayerPrefs.GetInt("Score" + i, highScore.scores[i]);
            PlayerPrefs.GetInt("collectibles" + i, highScore.collectibles[i]);

        }

        print(PlayerPrefs.GetInt("story_Mode_Completed"));

        loadedData.StoryModeCompleted = PlayerPrefs.GetInt("story_Mode_Completed");


        saveData = loadedData;


    }
}
