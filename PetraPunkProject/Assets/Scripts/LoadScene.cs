using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string selectedSceneName;
    
    public void SceneLoad()
    {
        SceneManager.LoadScene(selectedSceneName);
    }

}
