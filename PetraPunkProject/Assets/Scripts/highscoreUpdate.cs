using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscoreUpdate : MonoBehaviour
{
    public HighScoreVariable highscore;
    public GameObject scoreTextObject;
    [Range(-360.0f, 360.0f)]
    public float offsetY = 0;
    public int scoreTextHeight;
    public Canvas scoreCanvas;
    private int size;
    private string scoreTextTempPos;
    private string scoreTextTempNames;
    private string scoreTextTempScore;
    private string scoreTextTempScarab;
    private GameObject highscoreTextPos;
    private GameObject highscoreTextScore;
    private GameObject highscoreTextScarab;
    private bool highscoreLoaded = false;
    public bool showNames;
    public bool showScores;
    private string scorePoints;
    private string scoreCollectibles;
    private string scoreName;

    public FloatVariable inGameScore;
    public IntVariable collectiblesScore;
    public StringVariable playerName;


    private void OnEnable()
    {
        SaveData data = SaveLoadManager.LoadData(highscore);

        highscore.scores = data.scores;
        highscore.names = data.names;
        highscore.collectibles = data.collectibles;

        ShowHighscore();

    }

    public void ShowHighscore()
    {
        if (highscore != null)
        {
            size = highscore.scores.Length;

            scoreTextTempPos = "";
            scoreTextTempNames = "";
            scoreTextTempScore = "";
            scoreTextTempScarab = "";

            while (size > 0)
            {
                var pos = size;

                size--;

                scoreTextTempPos = pos + "\n" + scoreTextTempPos;

                if (showNames)
                {
                    scoreName = highscore.names[size].ToString() + "\n";
                    scoreTextTempNames = scoreName + scoreTextTempNames;
                }
                else
                {
                    scoreName = "";
                }

                if (showScores)
                {
                    scorePoints = highscore.scores[size].ToString() + "\n";
                    scoreTextTempScore = scorePoints + scoreTextTempScore;
                }
                else
                {
                    scorePoints = "";
                }
                scoreCollectibles = highscore.collectibles[size].ToString() + "\n";
                scoreTextTempScarab = scoreCollectibles + scoreTextTempScarab;
            }

            if (!highscoreLoaded)
            {
                //Pos text
                highscoreTextPos = Instantiate(scoreTextObject, scoreCanvas.transform);

                highscoreTextPos.GetComponent<RectTransform>().localPosition = new Vector3(-500, offsetY);
                highscoreTextPos.GetComponent<Text>().alignment = TextAnchor.UpperCenter;

                //Score Text
                highscoreTextScore = Instantiate(scoreTextObject, scoreCanvas.transform);

                highscoreTextScore.GetComponent<RectTransform>().localPosition = new Vector3(0, offsetY);
                highscoreTextScore.GetComponent<Text>().alignment = TextAnchor.UpperCenter;

                //Scarab Text
                highscoreTextScarab = Instantiate(scoreTextObject, scoreCanvas.transform);

                highscoreTextScarab.GetComponent<RectTransform>().localPosition = new Vector3(500, offsetY);
                highscoreTextScarab.GetComponent<Text>().alignment = TextAnchor.UpperCenter;

                highscoreLoaded = true;
            }

            highscoreTextPos.GetComponent<Text>().text = "<b>Pos:</b>" + "\n" + scoreTextTempPos;
            highscoreTextScore.GetComponent<Text>().text = "<b>Score:</b>" + "\n" + scoreTextTempScore;
            highscoreTextScarab.GetComponent<Text>().text = "<b>Scarabs:</b>" + "\n" + scoreTextTempScarab;
        }
    }

    public void UpdateHighscore()
    {
        var size = highscore.scores.Length - 1;

        var index = CheckHighscore();

        Debug.Log(index);
        Debug.Log(size);

        if (index != -1)
        {
            while (size > index)
            {
                highscore.names[size] = highscore.names[size-1];
                highscore.scores[size] = highscore.scores[size-1];
                highscore.collectibles[size] = highscore.collectibles[size - 1];

                size--;
            }
            highscore.names[index] = playerName.Value;
            highscore.scores[index] = (int)inGameScore.Value;
            highscore.collectibles[index] = (int)collectiblesScore.Value;
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
