using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_menu : MonoBehaviour
{
    public LocaleVariable CurrentLocale;

    public Object storyScene;
    public Object endlessScene;
    public Button endlessBt;
    public Text endlessBtText;
    public BoolVariable hasCompletedStory;

    public GameObject soundButton;
    public GameObject languageButton;
    public GameEvent languageChange;
    public FloatVariable volumeMusic;
    public FloatVariable volumeSfx;

    [Header("Audio")]
    public GameEvent startNewLevelAudio;
    public GameEvent buttonClickAudio;
    public GameEvent menuOpenAudio;
    public GameEvent menuCloseAudio;

    private bool sound;
    //public BoolVariable language;
    private GameEvent btClick;
    private GameEvent menuOpenClick;
    private GameEvent menuCloseClick;


   
    public HighScoreVariable highScore;
    SaveData saveData;

    private void Start()
    {   
        sound = true;

        //language.Value = true;

        btClick = buttonClickAudio;
        menuOpenClick = menuOpenAudio;
        menuCloseClick = menuCloseAudio;

        saveData = SaveLoadManager.LoadData(highScore);

        if (saveData.StoryModeCompleted == 1)
        {
            endlessBt.GetComponent<Button>().enabled = true;
            endlessBtText.GetComponent<Text>().color = new Color(255, 255, 255);
        }

        if (highScore == null)
        {
            /*
             * highScore = new HighScoreVariable();
             * highScore.scores = new int[5];
             * highScore.collectibles = new int[5];
             * highScore.names = new string[5];
            */
        }
    }

    public void mainMenuSelection()
    {
        btClick.Raise();
    }

    public void optionsMenuSelection(int screen)
    {
        btClick.Raise();

        switch (screen)
        {
            case 2:
                //btClick.Raise();

                languageChange.Raise();

                /*foreach (var children in languageButton.GetComponentsInChildren<Text>())
                {
                    children.text = $"language: {CurrentLocale.Value.name}";
                }
                */
 
                //language.Value = !language.Value;
                break;
            case 1:
                Debug.Log("Sound is: " + sound);
                btClick.Raise();
                if(sound == true)
                {
                    soundButton.GetComponentInChildren<Text>().text = "sound is:off";
                    AudioListener.pause = true;
                } else
                {
                    soundButton.GetComponentInChildren<Text>().text = "sound is:on";
                    AudioListener.pause = false;
                }
                sound = !sound;
                break;
        }
            
    }

    public void musicVolume(float vol)
    {
        volumeMusic.Value = vol;
    }

    public void sfxVolume(float vol)
    {
        volumeSfx.Value = vol;
    }


}
