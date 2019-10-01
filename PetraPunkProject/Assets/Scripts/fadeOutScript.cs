using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fadeOutScript : MonoBehaviour
{
    public bool IsFading = false;
    public BoolVariable hasWon;
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
            if (!hasWon.Value)
                hasWon.Value = true;
            var tempColor = screenImage.color;
            tempColor.a += fadeSpeed;
            screenImage.color = tempColor;

            if (screenImage.color.a >= 1.2)
            {
                SceneManager.LoadScene("EndScene");
            }

        }
    }
}
