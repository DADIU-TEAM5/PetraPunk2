using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Object selectedScene;
    
    public void SceneLoad(Object scene)
    {
        if(selectedScene == null && scene != null)
        {
            SceneManager.LoadScene(scene.name);
        }
        else if (selectedScene != null)
        {
            SceneManager.LoadScene(selectedScene.name);
        }
    }
}
