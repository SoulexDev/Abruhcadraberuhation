using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Source))]
public class InteractableSource : Interactable
{
    protected Source source;

    protected virtual void Awake()
    {
        source = GetComponent<Source>();
    }
}