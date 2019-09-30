﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeOutScript : MonoBehaviour
{
    public bool IsFading = false;
    public Image screenImage;
    public float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //screenImage = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFading)
        {
            var tempColor = screenImage.color;
            tempColor.a += fadeSpeed;
            screenImage.color = tempColor;

            Debug.Log(screenImage.color.a);
        }   
    }
}
