using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{
    public bool powered { get { return _powered; } set { _powered = value; OnPoweredChanged?.Invoke(); UpdateSockets(); } }
    private bool _powered;

    protected delegate void PoweredChanged();
    protected event PoweredChanged OnPoweredChanged;

    [SerializeField] protected List<Socket> sockets = new List<Socket>();
    protected virtual void Awake()
    {
        foreach (var socket in sockets)
        {
            socket.sources.Add(this);
        }
    }

    protected virtual void UpdateSockets()
    {
        foreach (var socket in sockets)
        {
            socket.CheckPowerState();
        }
    }
}