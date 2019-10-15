using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public FloatVariable Lifes;
    public Text lifeDisplay;
    public GameEvent death;
    public GameEvent deathAudio;
    public string SelectedScene;

    public Image Heart1, Heart2, Heart3;

    public Sprite GrayHeart;
    public Sprite RedHeart;

    public HighScoreVariable highScore;

    public int NumberOfLifes;

    private bool raisedDeath = false;

    string startString;
    // Start is called before the first frame update
    void Start()
    {
        Lifes.Value = NumberOfLifes;
        startString = lifeDisplay.text;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateHearts();
        //lifeDisplay.text = startString + Lifes.Value;

        if (Lifes.Value <= 0)
        {
            print("game Over");
            //SceneManager.LoadScene("EndlessLevel");
            
            if (!raisedDeath) {
                deathAudio.Raise();
                death.Raise();
                raisedDeath = true;

                if (highScore != null)
                {
                    SaveData saveData = new SaveData(highScore);

                    saveData.scores = highScore.scores;
                    saveData.collectibles = highScore.collectibles;
                    saveData.names = highScore.names;

                    saveData.StoryModeCompleted = 1;

                    SaveLoadManager.SaveData(saveData, highScore);
                }
            }
        } else {
            raisedDeath = false;
        }

        if (Lifes.Value <= 0 && raisedDeath == true && Input.GetMouseButton(0))
        {
            LoadScene();
        }

    }
    void UpdateHearts() {
        if (Lifes.Value == 0) {
            Heart1.sprite = GrayHeart;
            Heart2.sprite = GrayHeart;
            Heart3.sprite = GrayHeart;
        } else if (Lifes.Value == 1) {
            Heart1.sprite = RedHeart;
            Heart2.sprite = GrayHeart;
            Heart3.sprite = GrayHeart;
        } else if (Lifes.Value == 2) {
            Heart1.sprite = RedHeart;
            Heart2.sprite = RedHeart;
            Heart3.sprite = GrayHeart;
        } else if (Lifes.Value == 3) {
            Heart1.sprite = RedHeart;
            Heart2.sprite = RedHeart;
            Heart3.sprite = RedHeart;
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SelectedScene);
    }

    
}
