using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] private bool inverted;

    [HideInInspector] public bool powered;
    [HideInInspector] public List<Source> sources = new List<Source>();

    public delegate void CheckPower();
    public event CheckPower OnCheckPower;
    private void Awake()
    {
        powered = inverted;
    }
    public virtual void CheckPowerState()
    {
        powered = inverted != sources.All(s => s.powered == true);
        OnCheckPower?.Invoke();
    }
    public void OverridePowerState(bool state)
    {
        powered = inverted != state;
        OnCheckPower?.Invoke();
    }
}