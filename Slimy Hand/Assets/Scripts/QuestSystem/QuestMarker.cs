using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    [SerializeField] private GameObject enabler;
    private bool active;
    private Camera cam;

    private Vector3 pos;

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (active)
        {
            enabler.SetActive(Vector3.Dot(cam.transform.forward, pos - cam.transform.position) > 0);
        }
        if (gameObject.activeSelf)
        {
            transform.position = cam.WorldToScreenPoint(pos);
        }
    }
    public void Init(Vector3 pos)
    {
        this.pos = pos;
        active = true;

        gameObject.SetActive(true);
    }
    public void DestroyMarker()
    {
        active = false;
        pos = Vector3.zero;

        gameObject.SetActive(false);
    }
}