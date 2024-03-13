using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDoor : MonoBehaviour
{
    [SerializeField] private Transform door;
    private Quaternion startRot;
    private Quaternion endRot;

    private bool _open;
    private bool open
    {
        get
        {
            return _open;
        }
        set
        {
            if(_open != value)
            {
                _open = value;
                StopAllCoroutines();
                StartCoroutine(HandleDoor());
            }
        }
    }
    private void Awake()
    {
        startRot = door.localRotation;
    }
    private void Update()
    {
        open = Vector3.Distance(transform.position, Player.Instance.transform.position) < 4;
    }
    IEnumerator HandleDoor()
    {
        float timer = 0;
        endRot = open ? startRot * Quaternion.Euler(100, 0, 0) : startRot;
        while (timer < 1)
        {
            door.localRotation = Quaternion.Lerp(door.localRotation, endRot, timer);
            timer += Time.deltaTime * 2;
            yield return null;
        }
        door.localRotation = endRot;
    }
}