using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCaller : MonoBehaviour
{
    public GameEvent stepAudio;

    public void Step()
    {
        print("Step");
        stepAudio.Raise();
    }
}
