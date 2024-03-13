using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour
{
    public static LightingController Instance;

    [SerializeField] private LightingProfile lightingProfile;

    [SerializeField] private LightingProfile originalProfile;

    [SerializeField] private float transitionTime = 10;

    private void Awake()
    {
        Instance = this;
    }

    [ContextMenu("SetProfile")]
    public void SetProfile()
    {
        SetLightingProfile(lightingProfile);
    }
    [ContextMenu("ResetProfile")]
    public void ResetProfile()
    {
        SetLightingProfile(originalProfile);
    }
    public void SetLightingProfile(LightingProfile profile)
    {
        StartCoroutine(LerpSettings(profile));
    }
    IEnumerator LerpSettings(LightingProfile profile)
    {
        float timer = 0;
        Color ambientCol = profile.ambientColor * (Color.white * 0.4f);
        while (timer < transitionTime)
        {
            timer += Time.deltaTime;
            float lerp = timer / transitionTime;

            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, profile.fogDensity, lerp);
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, profile.fogColor, lerp);
            RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, ambientCol, lerp);
            yield return null;
        }

        RenderSettings.fogDensity = profile.fogDensity;
        RenderSettings.fogColor = profile.fogColor;
        RenderSettings.ambientLight = profile.ambientColor;
        RenderSettings.ambientLight *= 0.4f;
    }
}