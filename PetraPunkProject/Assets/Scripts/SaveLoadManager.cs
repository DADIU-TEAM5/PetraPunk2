using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    
    //public SaveData saveData;
    

    // Start is called before the first frame update

    

    public static void SaveData(SaveData saveData,HighScoreVariable highScore)
    {
        for (int i = 0; i < highScore.scores.Length; i++)
        {
            PlayerPrefs.SetString("Names" + i, saveData.names[i]);
            PlayerPrefs.SetInt("Score" + i, saveData.scores[i]);
            PlayerPrefs.SetInt("collectibles" + i, saveData.collectibles[i]);
        }

        Debug.Log("TEST: " + saveData.scores[0]);
        PlayerPrefs.SetInt("story_Mode_Completed", saveData.StoryModeCompleted);
        Debug.Log("StoryCompleted: " + saveData.StoryModeCompleted);
        PlayerPrefs.Save();
    }

    public static SaveData LoadData(HighScoreVariable highScore )
    {
        SaveData loadedData = new SaveData(highScore);


        for (int i = 0; i < highScore.scores.Length; i++)
        {
            loadedData.names[i] = PlayerPrefs.GetString("Names" + i);
            loadedData.scores[i] = PlayerPrefs.GetInt("Score" + i);
            loadedData.collectibles[i] = PlayerPrefs.GetInt("collectibles" + i);
        }

        print(PlayerPrefs.GetInt("story_Mode_Completed"));

        loadedData.StoryModeCompleted = PlayerPrefs.GetInt("story_Mode_Completed");


        return  loadedData;


    }
}
