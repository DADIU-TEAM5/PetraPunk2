﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Object selectedScene;
    
    // Start is called before the first frame update
    public void SceneLoad()
    {
        SceneManager.LoadScene(selectedScene.name);
    }
}
