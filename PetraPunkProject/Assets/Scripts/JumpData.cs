using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpData : MonoBehaviour
{
    public Animator anim;

    public float Height;
    public float AirTime;

    public AnimationCurve JumpCurve;


    public void PlayAnimation()
    {
        anim.SetTrigger("Jump");
    }
}
