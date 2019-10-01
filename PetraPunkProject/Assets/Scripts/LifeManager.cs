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
    public Object CreditsScene;

    public Image Heart1, Heart2, Heart3;

    public Sprite GrayHeart;
    public Sprite RedHeart;

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
        SceneManager.LoadScene(CreditsScene.name);
    }

    
}
