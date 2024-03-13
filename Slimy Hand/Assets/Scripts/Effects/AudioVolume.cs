using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] private Transform audioSource;
    [SerializeField] private BoxCollider col;

    private void Update()
    {
        Vector3 playerPos = Player.Instance.controller.transform.position;

        float clampedX = Mathf.Clamp(playerPos.x, col.bounds.min.x, col.bounds.max.x);
        float clampedY = Mathf.Clamp(playerPos.y, col.bounds.min.y, col.bounds.max.y);
        float clampedZ = Mathf.Clamp(playerPos.z, col.bounds.min.z, col.bounds.max.z);

        audioSource.position = new Vector3(clampedX, clampedY, clampedZ);
    }
}