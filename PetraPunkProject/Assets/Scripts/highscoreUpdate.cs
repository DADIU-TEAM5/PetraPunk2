using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscoreUpdate : MonoBehaviour
{
    public highScoreVariable highscore;
    public GameObject scoreTextObject;
    [Range(-360.0f, 360.0f)]
    public float offsetY = 0;
    public int scoreTextHeight;
    public Canvas scoreCanvas;
    private int size;
    private string scoreTextTemp;
    private GameObject highscoreText;
    private bool highscoreLoaded = false;

    public FloatVariable inGameScore;
    public StringVariable playerName;


    private void OnEnable()
    {
        ShowHighscore();
    }

    public void ShowHighscore()
    {
        size = highscore.scores.Length;

        scoreTextTemp = "";

        while (size > 0)
        {
            var pos = size;

            size--;

            scoreTextTemp = pos + "\t" + highscore.names[size].ToString() + "\t" + highscore.scores[size].ToString() + "\n" + scoreTextTemp;

        }

        if(!highscoreLoaded)
        {
            highscoreText = Instantiate(scoreTextObject, scoreCanvas.transform);

            highscoreText.GetComponent<RectTransform>().localPosition = new Vector3(0, offsetY);
            highscoreText.GetComponent<Text>().alignment = TextAnchor.UpperLeft;

            highscoreLoaded = true;
        }

        highscoreText.GetComponent<Text>().text = scoreTextTemp;
    }

    public void RemoveHighscore()
    {
        Destroy(highscoreText, 1f);
    }

    public void UpdateHighscore()
    {
        var size = highscore.scores.Length - 1;

        var index = CheckHighscore();

        if (index != -1)
        {
            while (size > index)
            {
                highscore.names[size] = highscore.names[size-1];
                highscore.scores[size] = highscore.scores[size-1];

                size--;
            }
            highscore.names[index] = playerName.Value;
            highscore.scores[index] = (int)inGameScore.Value;
        }
    }

    private int CheckHighscore ()
    {
        var i = highscore.scores.Length;

        var index = -1;

        while (i > 0)
        {
            i--;

            if (inGameScore.Value > highscore.scores[i])
            {
                index = i;
            }
        }

        return index;
    }
}
