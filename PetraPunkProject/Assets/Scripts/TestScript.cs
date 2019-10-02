using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestScript : MonoBehaviour
{

    public Text text;
    SaveData saveData;
    public highScoreVariable score;
    

    // Start is called before the first frame update
    void Start()
    {
        saveData = SaveLoadManager.LoadData(score);
    }

    // Update is called once per frame
    void Update()
    {
        if(saveData == null)
        {
            text.text = "no data";
        }
        else
        {
            text.text = saveData.StoryModeCompleted + "";
        }
    }
}
