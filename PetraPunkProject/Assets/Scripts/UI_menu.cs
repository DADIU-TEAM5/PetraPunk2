﻿using System.Collections;
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


    private void Start()
    {   
        sound = true;

        //language.Value = true;

        btClick = buttonClickAudio;
        menuOpenClick = menuOpenAudio;
        menuCloseClick = menuCloseAudio;

        if (hasCompletedStory.Value == true)
        {
            endlessBt.GetComponent<Button>().enabled = true;
            endlessBtText.GetComponent<Text>().color = new Color(255,255,255);
        }
        
    }

    public void mainMenuSelection(int screen)
    {
        switch (screen)
        {
            case 3:
                Debug.Log("Exit");
                btClick.Raise();
                Application.Quit();
                break;
            case 2:
                Debug.Log("Options");
                menuOpenClick.Raise();
                break;
            case 1:
                Debug.Log("new game");
                btClick.Raise();
                startNewLevelAudio.Raise();
                //SceneManager.LoadScene(2);
                break;
        }
    }

    public void optionsMenuSelection(int screen)
    {
        switch (screen)
        {
            case 3:
                Debug.Log("back");
                menuCloseClick.Raise();
                break;
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
