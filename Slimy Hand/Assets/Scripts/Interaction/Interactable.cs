using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public InteractionType interactionType;
    public string interactMessage;
    public bool isInteractable = true;
    public UnityEvent interactEvent;
    public virtual void Interact()
    {
        interactEvent?.Invoke();
    }
    public virtual void StartInteraction()
    {
        interactEvent?.Invoke();
    }
    public virtual void EndInteraction()
    {

    }
    public void SetInteractability(bool state)
    {
        isInteractable = state;
    }
}