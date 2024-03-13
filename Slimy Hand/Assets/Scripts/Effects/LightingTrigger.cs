using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTrigger : MonoBehaviour
{
    [SerializeField] private LightingProfile newProfile;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LightingController.Instance.SetLightingProfile(newProfile);
            gameObject.SetActive(false);
        }
    }
}