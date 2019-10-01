using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Object selectedScene;
    private bool runOnce;
    
    public void SceneLoad(Object scene)
    {
        /* if(selectedScene == null && scene != null)
        {
            SceneManager.LoadScene(scene.name);
        }
        else if (selectedScene != null)
        {
            SceneManager.LoadScene(selectedScene.name);
        }
        */
        if (!runOnce)
        {
            StartCoroutine(testanim(1f));

            runOnce = true;
        }

    }

    IEnumerator testanim (float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(selectedScene.name);
    }
}
