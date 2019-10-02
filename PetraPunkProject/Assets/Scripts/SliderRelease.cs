using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderRelease : MonoBehaviour
{
    public Slider slider;
    public GameEvent audioEvent;

    public void OnMouseUp()
    {
        audioEvent.Raise();
    }
}
