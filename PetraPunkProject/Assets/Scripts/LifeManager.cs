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
    public string CreditsScene;

    public Image Heart1, Heart2, Heart3;

    public Sprite GrayHeart;
    public Sprite RedHeart;

    public highScoreVariable highScore;

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
            
            if (!raisedDeath) {
                deathAudio.Raise();
                death.Raise();
                raisedDeath = true;
            }
        } else {
            raisedDeath = false;
        }

        if (Lifes.Value <= 0 && raisedDeath == true && Input.GetMouseButton(0))
        {


            SaveData saveData = new SaveData(highScore);


            highScore = new highScoreVariable();


            
                highScore.scores = saveData.scores;
                highScore.collectibles = saveData.collectibles;
                highScore.names = saveData.names;
            
            
            saveData.StoryModeCompleted = 1;

            SaveLoadManager.SaveData(saveData, highScore);

            LoadCredits();
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

    public void LoadCredits()
    {
        SceneManager.LoadScene(CreditsScene);
    }

    
}
