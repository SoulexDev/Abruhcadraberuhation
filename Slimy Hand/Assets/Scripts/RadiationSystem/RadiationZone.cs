using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationZone : MonoBehaviour
{
    [SerializeField] private float radiationDistance = 16;
    [SerializeField] private float radiationRate = 1;

    private float distanceFromPlayer;
    private void FixedUpdate()
    {
        distanceFromPlayer = (transform.position - Player.Instance.transform.position).magnitude;

        if (distanceFromPlayer <= radiationDistance)
        {
            Player.Instance.stats.Radiate((1 - Mathf.Clamp01(distanceFromPlayer / (radiationDistance * 0.5f))) * radiationRate * Time.deltaTime);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.4f);
        Gizmos.DrawSphere(transform.position, radiationDistance/2);
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}