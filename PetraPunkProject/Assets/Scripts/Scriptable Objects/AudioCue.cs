﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioCue : ScriptableObject
{
   
    public void Play(GameObject hitGameObject)
    {
        AkSoundEngine.PostEvent("PipeHit", hitGameObject);
    }
}
