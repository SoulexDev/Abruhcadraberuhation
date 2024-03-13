using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent triggerEvent;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            triggerEvent?.Invoke();

        gameObject.SetActive(false);
    }
}