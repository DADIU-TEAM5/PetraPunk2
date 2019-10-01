using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinLose : MonoBehaviour
{
    public BoolVariable hasWon;
    public GameObject UI_loseScene;
    public GameObject UI_winScene;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("EndScene Has Won: " + hasWon.Value);
        if(hasWon.Value)
        {
            UI_winScene.SetActive(true);
        } else
        {
            UI_loseScene.SetActive(true);
        }
    }
}
