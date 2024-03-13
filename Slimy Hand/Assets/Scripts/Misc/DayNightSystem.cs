using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DistantLands.Cozy;

public class DayNightSystem : MonoBehaviour
{
    [SerializeField] private CozyWeather weatherSystem;
    private float timeHour => weatherSystem.timeModule.currentTime * 24;
    private float previousTime;

    private void Update()
    {
        if(timeHour >= 6 && previousTime < 6)
        {
            SwitchToDay();
        }
        if(timeHour >= 18 && previousTime < 18)
        {
            SwitchToNight();
        }
        previousTime = timeHour;
    }
    void SwitchToNight()
    {
        
    }
    void SwitchToDay()
    {
        
    }
}