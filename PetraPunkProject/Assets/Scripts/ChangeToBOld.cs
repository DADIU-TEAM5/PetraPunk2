using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToBOld : MonoBehaviour
{

    public BoolVariable OnSlope;
    Text text;
    int size;
    private void Start()
    {
        text = GetComponent<Text>();
        size = text.fontSize;
    }
    // Update is called once per frame
    void Update()
    {

        if (OnSlope.Value)
        {
            text.fontStyle = FontStyle.Bold;
            text.fontSize = size + 5;
        }
        else
        {
            text.fontStyle = FontStyle.Normal;
            text.fontSize = size;
        }
            
    }
}
