using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedlineController : MonoBehaviour
{
    public PlayerController pc;
    public GameObject speedLines;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        speedLines.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(pc.OnSlope && !isActive)
        {
            speedLines.SetActive(true);
            isActive = true;
        }

        if (!pc.OnSlope && isActive)
        {
            speedLines.SetActive(false);
            isActive = false;
        }


    }
}
