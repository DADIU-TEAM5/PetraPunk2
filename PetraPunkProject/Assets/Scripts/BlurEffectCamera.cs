﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurEffectCamera : MonoBehaviour
{
    public Material effectMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, effectMaterial);
    }
}