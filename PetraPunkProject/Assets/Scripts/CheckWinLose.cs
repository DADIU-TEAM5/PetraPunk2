using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWinLose : MonoBehaviour
{
    public BoolVariable hasWon;
    public BoolVariable hasCompletedStory;
    public GameObject UI_loseScene;
    public GameObject UI_winScene;
    public highScoreVariable highScore;

    SaveData saveData;
   

    // Start is called before the first frame update
    void Start()
        
    {
        saveData = new SaveData(highScore);
        Debug.Log("EndScene Has Won: " + hasWon.Value);
        if(hasWon.Value == true)
        {
            UI_winScene.SetActive(true);
            hasCompletedStory.Value = true;
            saveData.StoryModeCompleted = 1;

            SaveLoadManager.SaveData(saveData,highScore);

            

        } else
        {
            UI_loseScene.SetActive(true);
        }
    }
}
