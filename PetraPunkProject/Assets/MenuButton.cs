using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Animator anim;

    IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(1.2F);
        SceneManager.LoadScene("UI_menu");

    }

    public void menuClick()
    {
        anim.SetBool("IsClicked", true);
        StartCoroutine(LoadMenuScene());

    }
}
