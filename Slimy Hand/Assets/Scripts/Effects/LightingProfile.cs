using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightingProfile : ScriptableObject
{
    public float fogDensity;
    public Color fogColor;
    public Color32 ambientColor;
}