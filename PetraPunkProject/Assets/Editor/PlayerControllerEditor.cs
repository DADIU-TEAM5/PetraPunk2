using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{

    PlayerController playerController;
    // Start is called before the first frame update


    private void OnEnable()
    {

        playerController = (PlayerController) target;
    }

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
    }

}
